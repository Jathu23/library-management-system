namespace library_management_system.Database.Entiy
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int BookId { get; set; } // Foreign Key to Book
        public int UserId { get; set; } // Foreign Key to User
        public string ReviewText { get; set; }
        public int Rating { get; set; } // Rating scale (e.g., 1-5)
        public string BookType { get; set; } // "Physical", "Ebook", "Audiobook"
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public NormalBook NormalBook { get; set; } 
        public Ebook EBook { get; set; } 
        public Audiobook AudioBook { get; set; } 
        public User User { get; set; } // The user who wrote the review
    }

}
