namespace AspNetApp.Models

{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }


        public ICollection<GymClass> GymClasses { get; set; } = new List<GymClass>();

        public ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();
    }
}
