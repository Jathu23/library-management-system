using library_management_system.Database;
using library_management_system.Database.Entiy;
using library_management_system.Database.Entiy.ReviewEntitys;
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

        // Add Normal Book Review
        public async Task<bool> AddNormalBookReviewAsync(NormalBookReview review)
        {
            await _context.NormalBookReviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return true;
        }

        // Add Ebook Review
        public async Task<bool> AddEbookReviewAsync(EbookReview review)
        {
            await _context.EbookReviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return true;
        }

        // Add Audiobook Review
        public async Task<bool> AddAudiobookReviewAsync(AudiobookReview review)
        {
            await _context.AudiobookReviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get Reviews for Normal Book
        public async Task<List<NormalBookReview>> GetNormalBookReviewsAsync(int bookId)
        {
            return await _context.NormalBookReviews
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }

        // Get Reviews for Ebook
        public async Task<List<EbookReview>> GetEbookReviewsAsync(int bookId)
        {
            return await _context.EbookReviews
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }

        // Get Reviews for Audiobook
        public async Task<List<AudiobookReview>> GetAudiobookReviewsAsync(int bookId)
        {
            return await _context.AudiobookReviews
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }





    }


}