using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetApp.Models
{
    public class UserMembership
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("Membership")]
        public int MembershipId { get; set; }

        public Membership Membership { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    }
}
