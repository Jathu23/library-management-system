using library_management_system.Database;
using library_management_system.Database.Entiy;
using library_management_system.Database.Entiy.LikeDisLike;
using library_management_system.DTOs;
using library_management_system.Repositories;
using static library_management_system.Controllers.LikeDislikeController;

namespace library_management_system.Services
{
    public class LikeDislikeService
    {
        private readonly LikeDislikeRepository _likeDislikeRepository;

        public LikeDislikeService(LikeDislikeRepository likeDislikeRepository)
        {
            _likeDislikeRepository = likeDislikeRepository;
        }


        // Add Normal Book Like/Dislike
        public async Task<ApiResponse<bool>> AddNormalBookLikeDislikeAsync(NormalBookLikeDislikeDto likeDislike)
        {
            var response = new ApiResponse<bool>();

            try
            {
                var userHasLikedDisliked = await _likeDislikeRepository.UserHasLikedDislikedNormalBookAsync(likeDislike.UserId, likeDislike.BookId);
                if (userHasLikedDisliked)
                {
                    response.Success = false;
                    response.Message = "You have already liked or disliked this book.";
                    return response;
                }
                var likedata = new NormalBookLikeDislike
                {
                    BookId = likeDislike.BookId,
                    UserId = likeDislike.UserId,
                    IsLiked = likeDislike.IsLiked
                };
                var isAdded = await _likeDislikeRepository.AddNormalBookLikeDislikeAsync(likedata);

                if (!isAdded)
                {
                    response.Success = false;
                    response.Message = "Failed to add like/dislike.";
                    return response;
                }

                response.Success = true;
                response.Message = "Like/Dislike added successfully.";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while adding the like/dislike.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        // Add Ebook Like/Dislike
        public async Task<ApiResponse<bool>> AddEbookLikeDislikeAsync(EbookLikeDislikeDto likeDislike)
        {
            var response = new ApiResponse<bool>();

            try
            {
                var userHasLikedDisliked = await _likeDislikeRepository.UserHasLikedDislikedEbookAsync(likeDislike.UserId, likeDislike.BookId);
                if (userHasLikedDisliked)
                {
                    response.Success = false;
                    response.Message = "You have already liked or disliked this ebook.";
                    return response;
                }
                var likedata = new EbookLikeDislike
                {
                    BookId = likeDislike.BookId,
                    UserId = likeDislike.UserId,
                    IsLiked = likeDislike.IsLiked
                };
                var isAdded = await _likeDislikeRepository.AddEbookLikeDislikeAsync(likedata);
                if (!isAdded)
                {
                    response.Success = false;
                    response.Message = "Failed to add like/dislike.";
                    return response;
                }

                response.Success = true;
                response.Message = "Like/Dislike added successfully.";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while adding the like/dislike.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        // Add Audiobook Like/Dislike
        public async Task<ApiResponse<bool>> AddAudiobookLikeDislikeAsync(AudiobookLikeDislikeDto likeDislike)
        {
            var response = new ApiResponse<bool>();

            try
            {
                var userHasLikedDisliked = await _likeDislikeRepository.UserHasLikedDislikedAudiobookAsync(likeDislike.UserId, likeDislike.BookId);
                if (userHasLikedDisliked)
                {
                    response.Success = false;
                    response.Message = "You have already liked or disliked this audiobook.";
                    return response;
                }
                var likedata = new AudiobookLikeDislike
                {
                    BookId = likeDislike.BookId,
                    UserId = likeDislike.UserId,
                    IsLiked = likeDislike.IsLiked
                };
                var isAdded = await _likeDislikeRepository.AddAudiobookLikeDislikeAsync(likedata);
                if (!isAdded)
                {
                    response.Success = false;
                    response.Message = "Failed to add like/dislike.";
                    return response;
                }

                response.Success = true;
                response.Message = "Like/Dislike added successfully.";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while adding the like/dislike.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        // Get Like/Dislike count for Normal Book
        public async Task<ApiResponse<int>> GetNormalBookLikeDislikeCountAsync(int bookId, bool isLiked)
        {
            var response = new ApiResponse<int>();

            try
            {
                var count = await _likeDislikeRepository.GetNormalBookLikeDislikeCountAsync(bookId, isLiked);
                response.Success = true;
                response.Message = "Like/Dislike count fetched successfully.";
                response.Data = count;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching like/dislike count.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        // Get Like/Dislike count for Ebook
        public async Task<ApiResponse<int>> GetEbookLikeDislikeCountAsync(int bookId, bool isLiked)
        {
            var response = new ApiResponse<int>();

            try
            {
                var count = await _likeDislikeRepository.GetEbookLikeDislikeCountAsync(bookId, isLiked);
                response.Success = true;
                response.Message = "Like/Dislike count fetched successfully.";
                response.Data = count;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching like/dislike count.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        // Get Like/Dislike count for Audiobook
        public async Task<ApiResponse<int>> GetAudiobookLikeDislikeCountAsync(int bookId, bool isLiked)
        {
            var response = new ApiResponse<int>();

            try
            {
                var count = await _likeDislikeRepository.GetAudiobookLikeDislikeCountAsync(bookId, isLiked);
                response.Success = true;
                response.Message = "Like/Dislike count fetched successfully.";
                response.Data = count;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching like/dislike count.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }

}
