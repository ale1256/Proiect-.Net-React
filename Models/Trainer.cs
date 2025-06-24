namespace AspNetApp.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Expertise { get; set; } = string.Empty;

        // Navigation property for courses trainer teaches
        public ICollection<GymClass> GymClasses { get; set; } = new List<GymClass>();

    }
}