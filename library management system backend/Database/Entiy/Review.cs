namespace library_management_system.Database.Entiy
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int BookId { get; set; } // Foreign Key to the book
        public int UserId { get; set; } // Foreign Key to the user
        public string ReviewText { get; set; }
        public int Rating { get; set; } // Rating scale (e.g., 1-5)
        public BookType BookType { get; set; } // Enum to specify book type
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public NormalBook NormalBook { get; set; } // Navigation for NormalBook
        public Ebook Ebook { get; set; } // Navigation for Ebook
        public Audiobook Audiobook { get; set; } // Navigation for Audiobook
        public User User { get; set; } // The user who wrote the review
    }

}
