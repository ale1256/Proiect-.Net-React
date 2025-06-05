namespace AspNetApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<Client> Clients { get; set; } = new List<Client>();
        public Trainer Trainer { get; set; } = null!;
        public int TrainerId { get; set; }
    }
}