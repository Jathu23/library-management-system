using library_management_system.Database.Entiy;
using library_management_system.DTOs.AudioBook;
using library_management_system.DTOs;
using library_management_system.Repositories;
using library_management_system.Utilities;

namespace library_management_system.Services
{
    public class AudioBookService
    {
        private readonly AudioBookRepository _audioBookRepository;
        private readonly AudioBookFileService   _audioBookFileService;

        public AudioBookService(AudioBookRepository audioBookRepository, AudioBookFileService audioBookFileService)
        {
            _audioBookRepository = audioBookRepository;
            _audioBookFileService = audioBookFileService;
        }

        public async Task<ApiResponse<int>> AddAudiobook(AddAudiobookDto audiobookDto)
        {
           
            var filePath = await _audioBookFileService.SaveFileAsync(audiobookDto.AudioFile, "Audiobooks");
            var coverImagePath = await _audioBookFileService.SaveFileAsync(audiobookDto.CoverImage, "AudiobookCovers");

           
            var audiobook = new Audiobook
            {
                ISBN = audiobookDto.ISBN,
                Title = audiobookDto.Title,
                Author = audiobookDto.Author,
                Genre = audiobookDto.Genre,
                PublishYear = audiobookDto.PublishYear,
                FilePath = filePath,
                CoverImagePath = coverImagePath,
                AddedDate = DateTime.Now,
            };

           
            var metadata = new AudiobookMetadata
            {
                FileFormat = audiobookDto.FileFormat,
                Language = audiobookDto.Language,
                Narrator = audiobookDto.Narrator,
                Publisher = audiobookDto.Publisher,
                Description = audiobookDto.Description,
                DigitalRights = audiobookDto.DigitalRights,
                DownloadCount= 0,
                PlayCount =0,
                FileSize =21,
                DurationInSeconds = 12
            };

         
            var audiobookId = await _audioBookRepository.AddAudiobook(audiobook, metadata);

            return new ApiResponse<int>
            {
                Success = true,
                Message = "Audiobook added successfully",
                Data = audiobookId
            };
        }


        public async Task<ApiResponse<bool>> UpdateAudiobookAsync(UpdateAudiobookDto audiobookDto)
        {
          
            var existingAudiobook = await _audioBookRepository.GetAudiobookById(audiobookDto.Id);

            if (existingAudiobook == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Audiobook not found",
                    Data = false
                };
            }

            var existingMetadata = await _audioBookRepository.GetAudiobookMetadataByAudiobookId(audiobookDto.Id);

            existingAudiobook.Title = audiobookDto.Title ?? existingAudiobook.Title;
            existingAudiobook.Author = audiobookDto.Author ?? existingAudiobook.Author;
            existingAudiobook.Genre = audiobookDto.Genre ?? existingAudiobook.Genre;
            existingAudiobook.PublishYear = audiobookDto.PublishYear ?? existingAudiobook.PublishYear;

            // Update file paths if new files are provided
            if (audiobookDto.AudioFile != null)
            {
                existingAudiobook.FilePath = await _audioBookFileService.SaveFileAsync(audiobookDto.AudioFile, "Audiobooks");
                existingMetadata.FileSize =  12;
                existingMetadata.DurationInSeconds =12;
            }

            if (audiobookDto.CoverImage != null)
            {
                existingAudiobook.CoverImagePath = await _audioBookFileService.SaveFileAsync(audiobookDto.CoverImage, "AudiobookCovers");
            }

           
            existingMetadata.Language = audiobookDto.Language ?? existingMetadata.Language;
            existingMetadata.Narrator = audiobookDto.Narrator ?? existingMetadata.Narrator;
            existingMetadata.Publisher = audiobookDto.Publisher ?? existingMetadata.Publisher;
            existingMetadata.Description = audiobookDto.Description ?? existingMetadata.Description;
            existingMetadata.DigitalRights = audiobookDto.DigitalRights ?? existingMetadata.DigitalRights;

           
            await _audioBookRepository.UpdateAudiobook(existingAudiobook, existingMetadata);

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Audiobook updated successfully",
                Data = true
            };
        }


        public async Task<ApiResponse<string>> DeleteAudiobook(int audiobookId)
        {
            try
            {
                var result = await _audioBookRepository.DeleteAudiobook(audiobookId);
                if (!result)
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Audiobook not found",
                        Errors = new List<string> { "Audiobook with the specified ID does not exist." }
                    };
                }

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Audiobook and associated metadata deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "An error occurred while deleting the audiobook.",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<PaginatedResult<AudiobookDto>>> GetAudiobooksAsync(int page, int pageSize)
        {
            // Fetch data and total count from repository
            var (audiobooks, totalCount) = await _audioBookRepository.GetPaginatedAudiobooksAsync(page, pageSize);

            // Map entities to DTOs
            var audiobookDtos = audiobooks.Select(a => new AudiobookDto
            {
                Id = a.Id,
                Title = a.Title,
                Author = a.Author,
                Genre = a.Genre,
                FilePath = a.FilePath,
                CoverImagePath = a.CoverImagePath,
                PublishYear = a.PublishYear,
                Language = a.Metadata.Language,
                Narrator = a.Metadata.Narrator,
                Publisher = a.Metadata.Publisher
            }).ToList();

            // Prepare paginated result
            var result = new PaginatedResult<AudiobookDto>
            {
                Items = audiobookDtos,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize
            };

            return new ApiResponse<PaginatedResult<AudiobookDto>>
            {
                Success = true,
                Message = "Audiobooks retrieved successfully.",
                Data = result
            };
        }

        public async Task<ApiResponse<PaginatedResult<AudiobookDto>>> SearchAudioBooksAsync(string searchString,int pageNumber,int pageSize)
        {
            try
            {
                var (audioBooks, totalRecords) = await _audioBookRepository.SearchAsync(searchString, pageNumber, pageSize);

                var audiobookDtos = audioBooks.Select(a => new AudiobookDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Author = a.Author,
                    Genre = a.Genre,
                    FilePath = a.FilePath,
                    CoverImagePath = a.CoverImagePath,
                    PublishYear = a.PublishYear,
                    Language = a.Metadata.Language,
                    Narrator = a.Metadata.Narrator,
                    Publisher = a.Metadata.Publisher
                }).ToList();

                var paginatedResult = new PaginatedResult<AudiobookDto>
                {
                    Items = audiobookDtos,
                    TotalCount = totalRecords,
                    CurrentPage = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponse<PaginatedResult<AudiobookDto>>
                {
                    Success = true,
                    Message = "AudioBooks retrieved successfully.",
                    Data = paginatedResult
                };
            }
            catch (Exception ex)
            {
              
                return new ApiResponse<PaginatedResult<AudiobookDto>>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

    }
}
