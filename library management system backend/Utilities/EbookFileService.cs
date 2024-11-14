using PdfSharp.Pdf.IO;
using VersOne.Epub;

namespace library_management_system.Utilities
{
    public class EbookFileService
    {


        //public async Task<string> SaveEbookFile(IFormFile ebookFile, string destinationFolder)
        //{
        //    string filePath = Path.Combine(destinationFolder, ebookFile.FileName);
        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await ebookFile.CopyToAsync(stream);
        //    }
        //    return filePath;
        //}


        //public double GetFileSizeInMB(IFormFile ebookFile)
        //{
        //    return ebookFile.Length / (1024.0 * 1024.0);
        //}


        //public int? GetPdfPageCount(string pdfFilePath)
        //{
        //    using (var document = PdfReader.Open(pdfFilePath, PdfDocumentOpenMode.InformationOnly))
        //    {
        //        return document.PageCount;
        //    }
        //}


        //public int? GetEpubPageCount(string epubFilePath)
        //{
        //    var epubBook = EpubReader.ReadBook(epubFilePath);
        //    return epubBook?.ReadingOrder?.Count; 
        //}


        //public int? GetPageCount(string filePath)
        //{
        //    string extension = Path.GetExtension(filePath).ToLower();

        //    return extension switch
        //    {
        //        ".pdf" => GetPdfPageCount(filePath),
        //        ".epub" => GetEpubPageCount(filePath),
        //        _ => null 
        //    };
        //}


        private readonly string _fileStoragePath = "EbookFiles"; // Set your file storage path here

        public async Task<string> SaveEbookFile(IFormFile ebookFile)
        {
            // Ensure file storage directory exists
            Directory.CreateDirectory(_fileStoragePath);

            // Generate a unique file path
            var filePath = Path.Combine(_fileStoragePath, ebookFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ebookFile.CopyToAsync(stream);
            }
            return filePath;
        }

        //public async Task<List<string>> SaveCoverImagesAsync(List<IFormFile> coverImages)
        //{
        //    var coverImagePaths = new List<string>();

        //    // Ensure file storage directory exists
        //    var coverImageStoragePath = Path.Combine(_fileStoragePath, "coverImages");
        //    Directory.CreateDirectory(coverImageStoragePath);

        //    foreach (var coverImage in coverImages)
        //    {
        //        var coverImagePath = Path.Combine(coverImageStoragePath, coverImage.FileName);
        //        using (var stream = new FileStream(coverImagePath, FileMode.Create))
        //        {
        //            await coverImage.CopyToAsync(stream);
        //        }
        //        coverImagePaths.Add(coverImagePath);
        //    }
        //    return coverImagePaths;
        //}

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
