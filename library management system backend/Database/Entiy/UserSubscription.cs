using System.ComponentModel.DataAnnotations;

namespace library_management_system.Database.Entiy
{
    public class UserSubscription
    {
        [Key]
        public int Id { get; set; }                 // Primary key
        public int UserId { get; set; }            // Foreign key to User
        public int SubscriptionPlanId { get; set; } // Foreign key to SubscriptionPlan
        public DateTime StartDate { get; set; }    // Subscription start date
        public DateTime EndDate { get; set; }      // Subscription end date
        public string Status { get; set; }         // Active, Expired, Cancelled, etc.
        public DateTime CreatedDate { get; set; } // When the record was created
        public DateTime UpdatedDate { get; set; } // When the record was last updated


        // Navigation properties
        public User User { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }

}
