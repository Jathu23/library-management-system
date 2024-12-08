using library_management_system.Database;
using library_management_system.Database.Entiy;
using library_management_system.Database.Entiy.LikeDisLike;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class LikeDislikeRepository
    {
        private readonly LibraryDbContext _context;

        public LikeDislikeRepository(LibraryDbContext context)
        {
            _context = context;
        }

        // Add Normal Book Like/Dislike
        public async Task<bool> AddNormalBookLikeDislikeAsync(NormalBookLikeDislike likeDislike)
        {
            var likedata = new NormalBookLikeDislike
            {
                BookId = 1,
                UserId = 1,
                IsLiked = true
            };
            await _context.NormalBookLikeDislikes.AddAsync(likedata);
            await _context.SaveChangesAsync();
            return true;
        }

        // Add Ebook Like/Dislike
        public async Task<bool> AddEbookLikeDislikeAsync(EbookLikeDislike likeDislike)
        {
            await _context.EbookLikeDislikes.AddAsync(likeDislike);
            await _context.SaveChangesAsync();
            return true;
        }

        // Add Audiobook Like/Dislike
        public async Task<bool> AddAudiobookLikeDislikeAsync(AudiobookLikeDislike likeDislike)
        {
            await _context.AudiobookLikeDislikes.AddAsync(likeDislike);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get Like/Dislike count for Normal Book
        public async Task<int> GetNormalBookLikeDislikeCountAsync(int bookId, bool isLiked)
        {
            return await _context.NormalBookLikeDislikes
                .Where(l => l.BookId == bookId && l.IsLiked == isLiked)
                .CountAsync();
        }

        // Get Like/Dislike count for Ebook
        public async Task<int> GetEbookLikeDislikeCountAsync(int bookId, bool isLiked)
        {
            return await _context.EbookLikeDislikes
                .Where(l => l.BookId == bookId && l.IsLiked == isLiked)
                .CountAsync();
        }

        // Get Like/Dislike count for Audiobook
        public async Task<int> GetAudiobookLikeDislikeCountAsync(int bookId, bool isLiked)
        {
            return await _context.AudiobookLikeDislikes
                .Where(l => l.BookId == bookId && l.IsLiked == isLiked)
                .CountAsync();
        }

        // Check if User has Liked/Disliked Normal Book
        public async Task<bool> UserHasLikedDislikedNormalBookAsync(int userId, int bookId)
        {
            return await _context.NormalBookLikeDislikes
                .AnyAsync(l => l.UserId == userId && l.BookId == bookId);
        }

        // Check if User has Liked/Disliked Ebook
        public async Task<bool> UserHasLikedDislikedEbookAsync(int userId, int bookId)
        {
            return await _context.EbookLikeDislikes
                .AnyAsync(l => l.UserId == userId && l.BookId == bookId);
        }

        // Check if User has Liked/Disliked Audiobook
        public async Task<bool> UserHasLikedDislikedAudiobookAsync(int userId, int bookId)
        {
            return await _context.AudiobookLikeDislikes
                .AnyAsync(l => l.UserId == userId && l.BookId == bookId);
        }
    }

}
