namespace AspNetApp.Models
{
    public class ClassBooking
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int GymClassId { get; set; }
        public GymClass GymClass { get; set; }

        public DateTime BookedAt { get; set; } = DateTime.UtcNow;
    }
}