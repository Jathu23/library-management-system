namespace library_management_system.Database.Entiy
{
    public class PaymentDuration
    {
        public int Id { get; set; } // Primary Key
        public string Duration { get; set; } // 1 Month, 3 Months, etc.
        public decimal Multiplier { get; set; } // Price multiplier
        public DateTime CreatedDate { get; set; } // When the duration was created
        public DateTime UpdatedDate { get; set; } // When the duration was last updated
    }

}
