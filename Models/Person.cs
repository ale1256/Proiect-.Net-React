namespace AspNetApp.Models
{
    public class Person
    {
        public int Id { get; set; }  // This is now the required OData key

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

    }
}
