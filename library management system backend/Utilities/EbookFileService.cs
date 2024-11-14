using PdfSharp.Pdf.IO;
using VersOne.Epub;

namespace library_management_system.Utilities
{
    public class EbookFileService
    {
        private readonly IWebHostEnvironment _environment;


        public EbookFileService(IWebHostEnvironment hostEnvironment)
        {
            _environment = hostEnvironment;
        }

        public async Task<string> SaveEbookFile(IFormFile file, string folderName)
        {
            var folderPath = Path.Combine(_environment.WebRootPath, folderName);
            Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(folderName, fileName);
        }


        public  async Task<double> GetFileSize(IFormFile file)
        {
            // Calculate file size in MB
            return file.Length / (1024.0 * 1024.0);
        }

        public async Task<int> GetPageCount(IFormFile file)
        {
            if (Path.GetExtension(file.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return await GetPdfPageCount(file);
            }
            else if (Path.GetExtension(file.FileName).Equals(".epub", StringComparison.OrdinalIgnoreCase))
            {
                return await GetEpubPageCount(file);
            }
            return 0;
        }

        private async Task<int> GetPdfPageCount(IFormFile file)
        {
            int pageCount = 0;
            using (var stream = file.OpenReadStream())
            {
                var pdfDoc = PdfReader.Open(stream, PdfDocumentOpenMode.InformationOnly);
                pageCount = pdfDoc.PageCount;
            }
            return pageCount;
        }

        private async Task<int> GetEpubPageCount(IFormFile file)
        {
            int pageCount = 0;
            using (var stream = file.OpenReadStream())
            {
                var epubBook = EpubReader.ReadBook(stream);
                pageCount = epubBook.ReadingOrder.Count;
            }
            return pageCount;
        }

    }
}
