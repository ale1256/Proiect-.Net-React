namespace AspNetApp.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerMonth { get; set; }
        public int DurationInMonths { get; set; }
    }
}
