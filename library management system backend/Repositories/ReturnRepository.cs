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


        public async Task ReturnLentBook( LentRecord lentRecord,RentHistory rentHistory,BookCopy bookCopy)
        {
           
            _context.LentRecords.Remove(lentRecord);

            
            _context.RentHistory.Update(rentHistory);

           
            _context.BookCopies.Update(bookCopy);

           
            await _context.SaveChangesAsync();
        }


        public async Task<LentRecord?> GetLentRecordById(int lentRecordId)
        {
            return await _context.LentRecords.FindAsync(lentRecordId);
        }
        public async Task<BookCopy?> GetBookCopyById(int copyId)
        {
            return await _context.BookCopies.FirstOrDefaultAsync(bc => bc.CopyId == copyId);
        }
        public async Task<NormalBook?> GetNormalBookById(int bookId)
        {
            return await _context.NormalBooks.FindAsync(bookId);
        }
        public async Task<RentHistory?> GetRentHistoryByLentRecordId(int lentRecordId)
        {
            return await _context.RentHistory.FindAsync(lentRecordId);
        }




    }
}
