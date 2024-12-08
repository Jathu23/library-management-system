namespace library_management_system.Database.Entiy.ReviewEntitys
{
    public class NormalBookReview
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }

        // Navigation Property for User
        public User User { get; set; }
        public NormalBook Book { get; set; }
    }

}
