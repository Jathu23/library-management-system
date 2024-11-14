using library_management_system.DTOs.Ebook;

namespace library_management_system.DTOs.Book
{
    public class AddBookDto
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public List<string> Genre { get; set; }
        public int PublishYear { get; set; }
        public string ShelfLocation { get; set; }
        public int TotalCopies { get; set; }
        public List<IFormFile>? CoverImages { get; set; }

        public EbookMetadataDto Metadata { get; set; }  // Metadata for the ebook
    }

}
