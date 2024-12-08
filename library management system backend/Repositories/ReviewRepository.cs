using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class ReviewRepository
    {
        private readonly LibraryDbContext _context;

        public ReviewRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Review?> GetByUserAndBookAsync(int userId, int bookId, string bookType)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.BookId == bookId && r.BookType == bookType);
        }

        public async Task<List<Review>> GetByBookAsync(int bookId, string bookType)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId && r.BookType == bookType)
                .ToListAsync();
        }

        public async Task AddOrUpdateAsync(Review review)
        {
            var existingReview = await GetByUserAndBookAsync(review.UserId, review.BookId, review.BookType);

            if (existingReview != null)
            {
                existingReview.ReviewText = review.ReviewText;
                existingReview.Rating = review.Rating;
                existingReview.CreatedDate = DateTime.Now;
                _context.Reviews.Update(existingReview);
            }
            else
            {
                _context.Reviews.Add(review);
            }

            await _context.SaveChangesAsync();
        }
    }
}
