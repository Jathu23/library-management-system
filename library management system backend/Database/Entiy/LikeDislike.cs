namespace library_management_system.Database.Entiy
{
    public class LikeDislike
    {
        public int LikeDislikeId { get; set; }
        public int BookId { get; set; } // Foreign Key to Book
        public int UserId { get; set; } // Foreign Key to User
        public bool? IsLike { get; set; } // True for like, False for dislike, Null for no choice
        public string BookType { get; set; } // "Physical", "Ebook", "Audiobook"
        public DateTime CreatedDate { get; set; }


        // Navigation properties
        public NormalBook NormalBook { get; set; } // The book being liked/disliked
        public Ebook EBook { get; set; } // The book being liked/disliked
        public Audiobook AudioBook { get; set; } // The book being liked/disliked
        public User User { get; set; } // The user who liked/disliked the book
    }

}
