namespace library_management_system.Database.Entiy
{
    public class AudiobookMetadata
    {
        public int Id { get; set; }
        public string FileFormat { get; set; }
        public double FileSize { get; set; }
        public int DurationInSeconds { get; set; }  
        public string Language { get; set; }
        public int DownloadCount { get; set; }
        public int PlayCount { get; set; } = 0;
        public string Narrator { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string DigitalRights { get; set; }


        public int AudiobookId { get; set; }
        public Audiobook Audiobook { get; set; }
    }

}
