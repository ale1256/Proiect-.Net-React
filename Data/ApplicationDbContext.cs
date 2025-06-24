using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspNetApp.Models;
using AspNetApp;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> People { get; set; }
    public DbSet<Client> Clients { get; set; }

    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<UserMembership> UserMemberships { get; set; }
    public DbSet<GymClass> GymClasses { get; set; }

    public DbSet<ClassBooking> ClassBookings { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Many-to-many between Client and GymClass
        modelBuilder.Entity<Client>()
            .HasMany(c => c.GymClasses)
            .WithMany(g => g.Clients)
            .UsingEntity(j => j.ToTable("ClientGymClasses")); // Use clear table name

        // One-to-many: Trainer → GymClasses
        modelBuilder.Entity<GymClass>()
            .HasOne(g => g.Trainer)
            .WithMany(t => t.GymClasses)
            .HasForeignKey(g => g.TrainerId);

        // Seed Memberships
        modelBuilder.Entity<Membership>().HasData(
            new Membership { Id = 1, PlanName = "Basic", Description = "Access during staffed hours", PricePerMonth = 29.99M, DurationInMonths = 1 },
            new Membership { Id = 2, PlanName = "Premium", Description = "24/7 access + monthly trainer session", PricePerMonth = 49.99M, DurationInMonths = 1 },
            new Membership { Id = 3, PlanName = "Elite", Description = "Unlimited access + trainer + nutrition", PricePerMonth = 79.99M, DurationInMonths = 1 }
        );
        modelBuilder.Entity<Trainer>().HasData(
            new Trainer { Id = 1, FullName = "John Doe" },
            new Trainer { Id = 2, FullName = "Jane Smith" },
            new Trainer { Id = 3, FullName = "Alice Johnson" },
            new Trainer { Id = 4, FullName = "Bob Brown" }
        );
        modelBuilder.Entity<GymClass>().HasData(
       new GymClass
       {
           Id = 1,
           Name = "HIIT",
           TrainerName = "John Doe",
           StartTime = DateTime.Parse("2025-06-13T10:00:00Z").ToUniversalTime(),

           Capacity = 20,
           SpotsLeft = 20,
           TrainerId = 1
       },
       new GymClass
       {
           Id = 2,
           Name = "Yoga",
           TrainerName = "Jane Smith",
           StartTime = DateTime.Parse("2025-06-13T10:00:00Z").ToUniversalTime(),

           Capacity = 15,
           SpotsLeft = 15,
           TrainerId = 2
       },
        new GymClass
        {
            Id = 3,
            Name = "Pilates",
            TrainerName = "Jane Smith",
            StartTime = DateTime.Parse("2025-06-17T10:00:00Z").ToUniversalTime(),

            Capacity = 10,
            SpotsLeft = 7,
            TrainerId = 3
        },
         new GymClass
         {
             Id = 4,
             Name = "Zumba",
             TrainerName = "Bob Brown",
             StartTime = DateTime.Parse("2025-06-17T10:00:00Z").ToUniversalTime(),

             Capacity = 25,
             SpotsLeft = 25,
             TrainerId = 4
         },
         new GymClass
         {
             Id = 5,
             Name = "Strength Training",
             TrainerName = "Alice Johnson",
             StartTime = DateTime.Parse("2025-06-18T10:00:00Z").ToUniversalTime(),

             Capacity = 15,
             SpotsLeft = 15,
             TrainerId = 3
         },
            new GymClass
            {
                Id = 6,
                Name = "Cardio Blast",
                TrainerName = "John Doe",
                StartTime = DateTime.Parse("2025-06-18T10:00:00Z").ToUniversalTime(),

                Capacity = 20,
                SpotsLeft = 20,
                TrainerId = 1
            }

   );



    }


}

