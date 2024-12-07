namespace library_management_system.DTOs.LikeandReview
{
    public class ReviewRequest
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; } // Rating scale (1-5)
        public string BookType { get; set; } // "Physical", "Ebook", "Audiobook"
    }

}
