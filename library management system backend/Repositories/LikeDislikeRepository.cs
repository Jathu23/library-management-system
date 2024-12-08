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

      
    }

}
