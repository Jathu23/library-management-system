namespace library_management_system.DTOs.Subscription
{

    public class PaymentRequest
    {
        public int UserId { get; set; }
        public int SubscriptionPlanId { get; set; }
        public int PaymentDurationId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
