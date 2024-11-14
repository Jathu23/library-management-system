namespace library_management_system.Database.Entiy
{
    public class EbookMetadata
    {
        public int Id { get; set; }  // Primary Key
        public int EbookId { get; set; }  // Foreign key to Ebook table

        public string? FileFormat { get; set; }
        public double? FileSize { get; set; }
        public int? PageCount { get; set; }
        public string? Language { get; set; }
        public int? DownloadCount { get; set; }
        public int? ViewCount { get; set; }
        public string? Publisher { get; set; }
        public string? Description { get; set; }
        public string? DigitalRights { get; set; }

        // Navigation property to Ebook
        public Ebook Ebook { get; set; }
    }
}
