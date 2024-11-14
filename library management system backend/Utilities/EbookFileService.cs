using PdfSharp.Pdf.IO;
using VersOne.Epub;

namespace library_management_system.Utilities
{
    public class EbookFileService
    {

        private readonly string _fileStoragePath = "EbookFiles"; // Set your file storage path here

        public async Task<string> SaveEbookFile(IFormFile ebookFile)
        {
          
            Directory.CreateDirectory(_fileStoragePath);

          
            var filePath = Path.Combine(_fileStoragePath, ebookFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ebookFile.CopyToAsync(stream);
            }
            return filePath;
        }
       

        public double GetFileSize(IFormFile file)
        {
            // Calculate file size in MB
            return file.Length / (1024.0 * 1024.0);
        }

        public int GetPageCount(IFormFile file)
        {
            if (Path.GetExtension(file.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return GetPdfPageCount(file);
            }
            else if (Path.GetExtension(file.FileName).Equals(".epub", StringComparison.OrdinalIgnoreCase))
            {
                return GetEpubPageCount(file);
            }
            return 0;
        }

        private int GetPdfPageCount(IFormFile file)
        {
            int pageCount = 0;
            using (var stream = file.OpenReadStream())
            {
                var pdfDoc = PdfReader.Open(stream, PdfDocumentOpenMode.InformationOnly);
                pageCount = pdfDoc.PageCount;
            }
            return pageCount;
        }

        private int GetEpubPageCount(IFormFile file)
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
