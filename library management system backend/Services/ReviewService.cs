using library_management_system.Database.Entiy;
using library_management_system.Repositories;

namespace library_management_system.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepository;

        public ReviewService(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }


        public async Task AddOrUpdateReviewAsync(int userId, int bookId, string bookType, string reviewText, int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");

            var review = new Review
            {
                UserId = userId,
                BookId = bookId,
                BookType = bookType,
                ReviewText = reviewText,
                Rating = rating,
                CreatedDate = DateTime.Now
            };

            await _reviewRepository.AddOrUpdateAsync(review);
        }

        public async Task<List<Review>> GetReviewsByBookAsync(int bookId, string bookType)
        {
            return await _reviewRepository.GetByBookAsync(bookId, bookType);
        }

        public async Task<Review?> GetReviewByUserAndBookAsync(int userId, int bookId, string bookType)
        {
            return await _reviewRepository.GetByUserAndBookAsync(userId, bookId, bookType);
        }
    }
}
