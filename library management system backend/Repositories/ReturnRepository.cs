using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class ReturnRepository
    {
        private readonly LibraryDbContext _context;

        public ReturnRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<LentRecord?> GetLentRecordById(int lentRecordId)
        {
            return await _context.LentRecords.FindAsync(lentRecordId);
        }

    }
}
