using library_management_system.Database.Entiy;
using library_management_system.DTOs.Ebook;
using library_management_system.DTOs;
using library_management_system.Repositories;
using library_management_system.Utilities;

namespace library_management_system.Services
{
    public class EbookService
    {
        private readonly EbookRepository _ebookRepository;
        private readonly EbookFileService _ebookFileService;
        private readonly ImageService _imageService;

        public EbookService(EbookRepository ebookRepository, EbookFileService ebookFileService, ImageService imageService)
        {
            _ebookRepository = ebookRepository;
            _ebookFileService = ebookFileService;
            _imageService = imageService;
        }

        public async Task<ApiResponse<int>> AddNewEbookAsync(AddEbookDto ebookDto)
        {
            try
            {
                if (ebookDto.EbookFile == null || ebookDto.EbookFile.Length == 0)
                {
                    return new ApiResponse<int>
                    {
                        Success = false,
                        Message = "Ebook file is required.",
                        Errors = new List<string> { "Please upload a valid ebook file." }
                    };
                }

                // Save the ebook file
                string ebookFilePath = await _ebookFileService.SaveEbookFile(ebookDto.EbookFile);
                var coverImagesPath = await SaveCoverImage(ebookDto.CoverImages);
                double fileSizeInMB = _ebookFileService.GetFileSize(ebookDto.EbookFile);
                int? pageCount = _ebookFileService.GetPageCount(ebookDto.EbookFile);


                // Create Ebook and Metadata instances
                var ebook = new Ebook
                {
                    ISBN = ebookDto.ISBN,
                    Title = ebookDto.Title,
                    Author = ebookDto.Author,
                    Genre = ebookDto.Genre,
                    PublishYear = ebookDto.PublishYear,
                    FilePath = ebookFilePath,
                    CoverImagePath = coverImagesPath,
                    AddedDate = DateTime.Now,
                };

                var metadata = new EbookMetadata
                {
                    FileFormat = ebookDto.EbookFile.ContentType,
                    FileSize = fileSizeInMB,
                    PageCount = pageCount,
                    Language = ebookDto.Metadata.Language,
                    DownloadCount = 0,
                    ViewCount = 0,
                    Publisher = ebookDto.Metadata.Publisher,
                    Description = ebookDto.Metadata.Description,
                    DigitalRights = ebookDto.Metadata.DigitalRights
                };

                // Save Ebook and metadata to database
                int ebookId = await _ebookRepository.AddEbookWithMetadataAsync(ebook, metadata);

                return new ApiResponse<int> { Success = true, Message = "Ebook added successfully", Data = ebookId };






            }
            catch (Exception ex)
            {
                return new ApiResponse<int>
                {
                    Success = false,
                    Message = "An error occurred while adding the ebook",
                    Errors = new List<string> { ex.Message }
                };
            }
        }


        private async Task<string> SaveCoverImage(IFormFile? profileImage)
        {
            if (profileImage == null)
                return "EbookCoverImages/defaultimg.jpg";

            var image = new List<IFormFile> { profileImage };
            var imagePath = await _imageService.SaveImages(image, "EbookCoverImages");
            return imagePath.First();
        }
    }
}
