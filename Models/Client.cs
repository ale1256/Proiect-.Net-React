namespace AspNetApp.Models

{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

       
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}