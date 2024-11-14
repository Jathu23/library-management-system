namespace library_management_system.DTOs.AudioBook
{
    public class AddAudiobookDto
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PublishYear { get; set; }
        public IFormFile AudioFile { get; set; }
        public IFormFile CoverImage { get; set; }

      
        public string FileFormat { get; set; }
        public string Language { get; set; }
        public string Narrator { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string DigitalRights { get; set; }
    }

}
