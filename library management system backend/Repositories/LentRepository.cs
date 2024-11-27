using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace library_management_system.Repositories
{
    public class LentRepository
    {
        private readonly LibraryDbContext _context;

        public LentRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task LendNormalBook( LentRecord lentRecord,RentHistory rentHistory,BookCopy bookCopy,NormalBook book)
        {
           
            _context.LentRecords.Add(lentRecord);

     
            _context.RentHistory.Add(rentHistory);

          
            _context.BookCopies.Update(bookCopy);

          
            _context.NormalBooks.Update(book);

           
            await _context.SaveChangesAsync();
        }

        public async Task<NormalBook?> GetNormalBookWithCopies(int bookId)
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
        public async Task<NormalBook?> GetBookById(int id)
        {
            return await _context.NormalBooks.FindAsync(id);

        }
        public async Task<BookCopy?> GetBookCopyById(int bookCopyId)
        {
            return await _context.BookCopies.Include(bc => bc.Book).FirstOrDefaultAsync(bc => bc.CopyId == bookCopyId);
        }


        public async Task<LentRecord?> GetLentRecordWithDetailsAsync(int userid)
        {
            return await _context.LentRecords
                .Include(lr => lr.BookCopy) // Include the BookCopy related to the LentRecord
      
                .Include(lr => lr.User) // Include the User related to the LentRecord
                .Include(lr => lr.Admin) // Include the Admin related to the LentRecord
                .FirstOrDefaultAsync(lr => lr.UserId == userid);
        }

        public async Task<List<LentRecord>?> GetLentRecordWithDetailsbyuserid(int userid)
        {
            return await _context.LentRecords
                .Include(lr => lr.BookCopy)
                .Include(lr => lr.User)
                .Include(lr => lr.Admin)
                .Where(lr => lr.UserId == userid)
                .ToListAsync();
        }
        public async Task<(List<RentHistory>?,int)> GetAllRentHistory(int page, int pageSize)
        {
            var query =  _context.RentHistory
                .Include(lr => lr.BookCopy)
                .Include(lr => lr.User)
                .Include(lr => lr.Admin)
                .AsQueryable();

            var recods = await query.OrderBy(b => b.Id)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            int totalRecords = await query.CountAsync();

            return (recods, totalRecords);
        }

        public async Task<List<LentRecord>?> GetAllLentRecordsWithDetailsAsync()
        {
            return await _context.LentRecords
                .Include(lr => lr.BookCopy) 
                .Include(lr => lr.User)     // Include User details
                .Include(lr => lr.Admin)    // Include Admin details
                .ToListAsync();
        }



    }
}
