namespace library_management_system.DTOs.LikeandReview
{
    public class LikeDislikeRequest
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public bool? IsLike { get; set; } // True for like, False for dislike, Null for no choice
        public string BookType { get; set; } // "Physical", "Ebook", "Audiobook"
    }

}
