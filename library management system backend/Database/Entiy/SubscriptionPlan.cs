using System.ComponentModel.DataAnnotations;

namespace library_management_system.Database.Entiy
{
    public class SubscriptionPlan
    {
        [Key]
        public int Id { get; set; }                 // Primary key
        public string Name { get; set; }           // Plan name (Free, Standard, Premium)
        public int BorrowLimit { get; set; }       // Max books per 30 days
        public bool AccessEbooks { get; set; }     // Access to e-books
        public bool AccessAudiobooks { get; set; } // Access to audiobooks
        public DateTime CreatedDate { get; set; } // When the plan was created
        public DateTime UpdatedDate { get; set; } // When the plan was last updated
        public List<UserSubscription> UserSubscriptions { get; set; } // Navigation
    }

}
