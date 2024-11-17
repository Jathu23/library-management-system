namespace library_management_system.DTOs.Subscription
{
    public class GlobalSubscriptionDto
    {
        public int UserId { get; set; }
        public string SubscriptionType { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }

}
