using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetApp.Models
{
    public class UserMembership
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }  

        public Client Client { get; set; }  

        [ForeignKey("Membership")]
        public int MembershipId { get; set; }

        public Membership Membership { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    }
}
