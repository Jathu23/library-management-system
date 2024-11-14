namespace library_management_system.DTOs.Ebook
{
    public class AddEbookDto
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PublishYear { get; set; }
        public IFormFile EbookFile { get; set; }
        public IFormFile? CoverImages { get; set; }
        public EbookMetadataDto Metadata { get; set; }
    }
}
