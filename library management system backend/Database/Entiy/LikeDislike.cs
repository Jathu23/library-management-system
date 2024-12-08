namespace library_management_system.Database.Entiy
{
    public class LikeDislike
    {
        public int LikeDislikeId { get; set; }
        public int BookId { get; set; } // Foreign Key to the book
        public int UserId { get; set; } // Foreign Key to the user
        public bool? IsLike { get; set; } // True for like, False for dislike, Null for no choice
        public BookType BookType { get; set; } // Enum to specify book type
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public NormalBook NormalBook { get; set; } // Navigation for NormalBook
        public Ebook Ebook { get; set; } // Navigation for Ebook
        public Audiobook Audiobook { get; set; } // Navigation for Audiobook
        public User User { get; set; } // The user who liked/disliked the book
    }


}
