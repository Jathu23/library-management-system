using library_management_system.Database.Entiy;
using library_management_system.Repositories;

namespace library_management_system.Services
{
    public class LikeDislikeService
    {
        private readonly LikeDislikeRepository _likeDislikeRepository;

        public LikeDislikeService(LikeDislikeRepository likeDislikeRepository)
        {
            _likeDislikeRepository = likeDislikeRepository;
        }

        public async Task AddOrUpdateLikeDislikeAsync(int userId, int bookId, string bookType, bool? isLike)
        {
            if (isLike == null)
                throw new ArgumentException("Like/Dislike status is required.");

            var likeDislike = new LikeDislike
            {
                UserId = userId,
                BookId = bookId,
                BookType = bookType,
                IsLike = isLike,
                CreatedDate = DateTime.Now
            };

            await _likeDislikeRepository.AddOrUpdateAsync(likeDislike);
        }

        public async Task<LikeDislike?> GetLikeDislikeAsync(int userId, int bookId, string bookType)
        {
            return await _likeDislikeRepository.GetByUserAndBookAsync(userId, bookId, bookType);
        }
    }
}
