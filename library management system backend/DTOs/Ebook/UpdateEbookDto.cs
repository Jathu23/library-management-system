namespace library_management_system.DTOs.Ebook
{
    public class UpdateEbookDto
    {
        public class EbookUpdateDto
        {
            public int Id { get; set; }             
            public string? Title { get; set; }
            public string? Author { get; set; }
            public string? Genre { get; set; }
            public int? PublishYear { get; set; }
            public string? Publisher { get; set; }
            public IFormFile? CoverImages { get; set; }
            public IFormFile? EbookFile { get; set; }
            public string? Language { get; set; }
            public string? Description { get; set; }
            public string? DigitalRights { get; set; }
        }


    }

}
