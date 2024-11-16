using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class LentRepository
    {
        private readonly LibraryDbContext _context;

        public LentRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task LendNormalBookAsync( LentRecord lentRecord,RentHistory rentHistory,BookCopy bookCopy,NormalBook book)
        {
           
            _context.LentRecords.Add(lentRecord);

          
            _context.RentHistory.Add(rentHistory);

          
            _context.BookCopies.Update(bookCopy);

          
            _context.NormalBooks.Update(book);

           
            await _context.SaveChangesAsync();
        }

        public async Task<NormalBook> GetNormalBookWithCopiesAsync(int bookId)
        {
            return await _context.NormalBooks.Include(b => b.BookCopies).FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
               
        }
        public async Task<Admin?> GetAdminById(int id)
        {
            return await _context.Admins.FindAsync(id);

        }


    }
}
