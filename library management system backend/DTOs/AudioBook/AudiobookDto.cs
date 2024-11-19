namespace library_management_system.DTOs.AudioBook
{
    public class AudiobookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string FilePath { get; set; }
        public string CoverImagePath { get; set; }
        public int PublishYear { get; set; }
        public string Language { get; set; }
        public string Narrator { get; set; }
        public string Publisher { get; set; }
    }

}
