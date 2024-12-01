namespace library_management_system.DTOs.Subscription
{
    public class SubscribeRequest
    {
        public int UserId { get; set; }
        public int SubscriptionPlanId { get; set; }
        public int PaymentDurationId { get; set; }
        public string PayMethod { get; set; }
    }
}
