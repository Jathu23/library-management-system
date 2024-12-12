using library_management_system.Database.Entiy;
using library_management_system.DTOs.Ebook;
using library_management_system.DTOs;
using library_management_system.Repositories;
using library_management_system.Utilities;
using static library_management_system.DTOs.Ebook.UpdateEbookDto;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ApiResponse<int>> AddNewEbook(AddEbookDto ebookDto)
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
                string ebookFilePath = await _ebookFileService.SaveEbookFile(ebookDto.EbookFile,"Ebooks");
                var coverImagesPath = await SaveCoverImage(ebookDto.CoverImages);
                double fileSizeInMB = await _ebookFileService.GetFileSize(ebookDto.EbookFile);
                int? pageCount = await _ebookFileService.GetPageCount(ebookDto.EbookFile);


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
                int ebookId = await _ebookRepository.AddEbook(ebook, metadata);

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

        public async Task<ApiResponse<string>> DeleteEbook(int ebookId)
        {
            var ebook = await _ebookRepository.GetEbookById(ebookId);
            if (ebook == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Ebook not found.",
                    Errors = new List<string> { "The specified ebook does not exist." }
                };
            }

            var success = await _ebookRepository.DeleteEbook(ebookId);

            return new ApiResponse<string>
            {
                Success = success,
                Message = success ? "Ebook deleted successfully." : "Failed to delete ebook."
            };
        }

        public async Task<ApiResponse<bool>> UpdateEbook(EbookUpdateDto ebookDto)
        {
            var existingEbook = await _ebookRepository.GetEbookById(ebookDto.Id);

            if (existingEbook == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Ebook not found",
                    Data = false
                };
            }

            var existingMetadata = await _ebookRepository.GetEbookMetadataByEbookId(ebookDto.Id);

            
            existingEbook.Title = ebookDto.Title ?? existingEbook.Title;
            existingEbook.Author = ebookDto.Author ?? existingEbook.Author;
            existingEbook.Genre = ebookDto.Genre ?? existingEbook.Genre;
            existingEbook.PublishYear = ebookDto.PublishYear ?? existingEbook.PublishYear;

           
            if (ebookDto.EbookFile != null)
            {
                existingEbook.FilePath = await _ebookFileService.SaveEbookFile(ebookDto.EbookFile, "Ebooks");
     
                existingMetadata.FileSize = await _ebookFileService.GetFileSize(ebookDto.EbookFile);
                existingMetadata.PageCount = await _ebookFileService.GetPageCount(ebookDto.EbookFile);
            }
                
                if (ebookDto.CoverImages != null)
                {
                   existingEbook.CoverImagePath = await SaveCoverImage(ebookDto.CoverImages);

                 }

           
            existingMetadata.Language = ebookDto.Language ?? existingMetadata.Language;
                existingMetadata.Publisher = ebookDto.Publisher ?? existingMetadata.Publisher;
                existingMetadata.Description = ebookDto.Description ?? existingMetadata.Description;
                existingMetadata.DigitalRights = ebookDto.DigitalRights ?? existingMetadata.DigitalRights;

                
                await _ebookRepository.UpdateEbook(existingEbook, existingMetadata);

                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Ebook updated successfully",
                    Data = true
                };
            

        }




        public async Task<ApiResponse<PaginatedResult<EbookDto>>> GetEbooksWithPagination(int pageNumber, int pageSize)
        {
            // Fetch paginated data from the repository
            var (ebooks, totalCount) = await _ebookRepository.GetPaginatedEbooks(pageNumber, pageSize);

            // Map to DTO
            var ebookDtos = ebooks.Select(e => new EbookDto
            {
                Id = e.Id,
                ISBN = e.ISBN,
                Title = e.Title,
                Author = e.Author,
                Genre = e.Genre,
                PublishYear = e.PublishYear,
                AddedDate = e.AddedDate,
                FilePath = e.FilePath,
                CoverImagePath = e.CoverImagePath,
                Metadata = new EbookMetadataDtoRes
                 {
                    FileFormat = e.Metadata.FileFormat,
                    FileSize = e.Metadata.FileSize,
                    PageCount = e.Metadata.PageCount,
                    Language = e.Metadata.Language,
                    DownloadCount = e.Metadata.DownloadCount,
                    ViewCount = e.Metadata.ViewCount,
                    Publisher = e.Metadata.Publisher,
                    Description = e.Metadata.Description,
                    DigitalRights = e.Metadata.DigitalRights
                }
               
            }).ToList();

            // Return API response with paginated results
            var paginatedResult = new PaginatedResult<EbookDto>
            {
                Items = ebookDtos,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            return new ApiResponse<PaginatedResult<EbookDto>>
            {
                Success = true,
                Message = "Ebooks fetched successfully.",
                Data = paginatedResult
            };
        }








        public async Task<string> SaveCoverImage(IFormFile? profileImage)
        {
            if (profileImage == null)
                return "EbookCoverImages/defaultimg.jpg";

            var image = new List<IFormFile> { profileImage };
            var imagePath = await _imageService.SaveImages(image, "EbookCoverImages");
            return imagePath.First();
        }


        public async Task<ApiResponse<PaginatedResult<EbookDto>>> SearchEbooksAsync(string searchString, int pageNumber, int pageSize)
        {
            try
            {
                var (ebooks, totalRecords) = await _ebookRepository.SearchEbooksAsync(searchString, pageNumber, pageSize);

                var ebookDtos = ebooks.Select(e => new EbookDto
                {
                    Id = e.Id,
                    ISBN = e.ISBN,
                    Title = e.Title,
                    Author = e.Author,
                    Genre = e.Genre,
                    PublishYear = e.PublishYear,
                    AddedDate = e.AddedDate,
                    FilePath = e.FilePath,
                    CoverImagePath = e.CoverImagePath,
                    Language = e.Metadata.Language,
                    Publisher = e.Metadata.Publisher
                }).ToList();

                var paginatedResult = new PaginatedResult<EbookDto>
                {
                    Items = ebookDtos,
                    TotalCount = totalRecords,
                    CurrentPage = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponse<PaginatedResult<EbookDto>>
                {
                    Success = true,
                    Message = "EBooks retrieved successfully.",
                    Data = paginatedResult
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PaginatedResult<EbookDto>>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<bool> AddClick(int bookid)
        {
            return await _ebookRepository.AddClick(bookid);

        }

        //DbFunctions for sorting top

        public async Task<List<Ebook>> GetTopEbooksAsync(int count)
        {
            try
            {
                // Try to get the top eBooks from the repository
                return await _ebookRepository.GetTopEbooksAsync(count);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("An error occurred while fetching top eBooks.", ex);
            }
        }

    }
}
