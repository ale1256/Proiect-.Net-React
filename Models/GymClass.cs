using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetApp.Models
{
    public class GymClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string TrainerName { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public int Capacity { get; set; }

        public int SpotsLeft { get; set; }

        // Foreign key to Trainer
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        // Many-to-many with Clients
        public ICollection<Client> Clients { get; set; } = new List<Client>();
    }
}
