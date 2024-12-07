using library_management_system.Database;
using library_management_system.Database.Entiy;
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

        public async Task<LikeDislike?> GetByUserAndBookAsync(int userId, int bookId, string bookType)
        {
            return await _context.LikeDislikes
                .FirstOrDefaultAsync(ld => ld.UserId == userId && ld.BookId == bookId && ld.BookType == bookType);
        }

        public async Task AddOrUpdateAsync(LikeDislike likeDislike)
        {
            var existingLikeDislike = await GetByUserAndBookAsync(likeDislike.UserId, likeDislike.BookId, likeDislike.BookType);

            if (existingLikeDislike != null)
            {
                existingLikeDislike.IsLike = likeDislike.IsLike;
                existingLikeDislike.CreatedDate = DateTime.Now;
                _context.LikeDislikes.Update(existingLikeDislike);
            }
            else
            {
                _context.LikeDislikes.Add(likeDislike);
            }

            await _context.SaveChangesAsync();
        }
    }
}
