namespace library_management_system.DTOs.Ebook
{
    public class EbookMetadataDtoRes
    {
        public string FileFormat { get; set; }
        public double? FileSize { get; set; }
        public int? PageCount { get; set; }
        public string Language { get; set; }
        public int? DownloadCount { get; set; }
        public int? ViewCount { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string DigitalRights { get; set; }
    }
}
