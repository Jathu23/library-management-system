namespace library_management_system.Database.Entiy
{
    public class Payment
    {
        public int Id { get; set; } // Primary Key
        public int UserId { get; set; } // Foreign Key from Users table
        public int SubscriptionPlanId { get; set; } // Foreign Key from SubscriptionPlans table
        public int PaymentDurationId { get; set; } // Foreign Key from PaymentDurations table
        public decimal AmountPaid { get; set; } // Total amount paid
        public DateTime PaymentDate { get; set; } // Date of payment
        public string PaymentMethod { get; set; } // e.g., Card, PayPal
        public string Status { get; set; } // Successful, Failed, Pending
        public string TransactionId { get; set; } // Unique transaction identifier
        public DateTime CreatedDate { get; set; } // When the payment record was created
        public DateTime UpdatedDate { get; set; } // When the payment record was last updated

        // Navigation properties
        public SubscriptionPlan SubscriptionPlan { get; set; }
        public PaymentDuration PaymentDuration { get; set; }
    }

}
