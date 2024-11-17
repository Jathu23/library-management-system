using System.ComponentModel.DataAnnotations;

namespace library_management_system.Database.Entiy
{

        public class GlobalSubscription
        {
            [Key]
            public int Id { get; set; }
            public int UserId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string SubscriptionType { get; set; } // "Monthly" or "Yearly"
            public decimal Amount { get; set; }
            public bool IsActive { get; set; } // Active during subscription period
            // Navigation property
            public User? User { get; set; }
        }

}


