namespace library_management_system.DTOs.Book
{
    public class UpdateBookDto
    {
        public int Id { get; set; }                  // Required to identify which book to update
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public List<string> Genre { get; set; }
        public int PublishYear { get; set; }
        public string ShelfLocation { get; set; }
        public int TotalCopies { get; set; }
        public List<IFormFile>? CoverImages { get; set; } // New cover images if any
    }
}
