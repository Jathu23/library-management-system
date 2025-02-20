﻿using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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


        public async Task<List<LentRecord>?> GetLentRecordWithDetailsbyuserid(int userid)
        {
            return await _context.LentRecords
                .Include(lr => lr.BookCopy)
                  .ThenInclude(lr => lr.Book)
                .Include(lr => lr.User)
                .Include(lr => lr.Admin)
                .Where(lr => lr.UserId == userid)
                .ToListAsync();
        }

        public async Task<List<LentRecord>?> GetAllLentRecordsWithDetailsAsync()
        {
            return await _context.LentRecords
                .Include(lr => lr.BookCopy)
                  .ThenInclude(lr => lr.Book)
                .Include(lr => lr.User)     // Include User details
                .Include(lr => lr.Admin)    // Include Admin details
                .ToListAsync();
        }
        public async Task<List<LentRecord>?> GetAllOverDuesWithDetailsAsync()
        {
            return await _context.LentRecords
                .Include(lr => lr.BookCopy)
                  .ThenInclude(lr => lr.Book)
                .Include(lr => lr.User)     // Include User details
                .Include(lr => lr.Admin)    // Include Admin details
                .Where(lr => lr.DueDate < DateTime.Now)
                .ToListAsync() ?? new List<LentRecord>(); 
        }
        public async Task<(List<RentHistory>?, int)> GetAllRentHistory(int page, int pageSize)
        {
            var query = _context.RentHistory
                .Include(rh => rh.BookCopy)
                .ThenInclude(rh => rh.Book)
                .Include(rh => rh.User)
                .Include(rh => rh.IssuingAdmin)  // Include Issuing Admin
                .Include(rh => rh.ReceivingAdmin) // Include Receiving Admin
                .AsQueryable();

            var records = await query
                .OrderBy(rh => rh.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int totalRecords = await query.CountAsync();

            return (records, totalRecords);
        }


       

        public async Task<List<LentRecord>> GetLentRecordsByUserIdAsync(int userId)
        {
            return await _context.LentRecords
                .Include(lr => lr.BookCopy)
                .Include(lr => lr.User)
                .Where(lr => lr.UserId == userId)
                .ToListAsync();
        }
        public async Task<List<LentRecord>> GetOverDueByUserIdAsync(int userId)
        {
            return await _context.LentRecords
                .Include(lr => lr.BookCopy)
                .Include(lr => lr.User)
                .Where(lr => lr.UserId == userId && lr.DueDate < DateTime.Now)
                .ToListAsync() ?? new List<LentRecord>();
        }


        public async Task<List<RentHistory>> GetRentHistoryByUser(int userId)
        {
            var records = await _context.RentHistory
                .Include(rh => rh.BookCopy)
                   .ThenInclude(rh => rh.Book)
                .Include(rh => rh.User)
                .Include(rh => rh.IssuingAdmin)
                .Include(rh => rh.ReceivingAdmin)
                .Where(rh => rh.UserId == userId)
                .OrderBy(rh => rh.Id)
                .ToListAsync();

            return records;
        }


        public async Task<List<RentHistory>> GetAllLentRecords(DateTime date)
        {
            return await _context.RentHistory
                .Include(r => r.BookCopy)
                .ThenInclude(bc => bc.Book)
                .Include(r => r.User)
                .Include(r => r.IssuingAdmin)
                .Include(r => r.ReceivingAdmin)
             
                .ToListAsync();
        }
        public async Task<List<RentHistory>> GetAllLentRecordsbyuserid(int userid)
        {
            return await _context.RentHistory
                .Include(r => r.BookCopy)
                .ThenInclude(bc => bc.Book)
                .Include(r => r.User)
                .Include(r => r.IssuingAdmin)
                .Include(r => r.ReceivingAdmin)
                .Where(r => r.UserId == userid)
                .ToListAsync();
        }

        public async Task<List<NormalBook>> GetAllBooksWithLendingDetailsAsync()
        {
            return await _context.NormalBooks
                .Include(b => b.BookCopies)
                    .ThenInclude(bc => bc.RentHistories)
                        .ThenInclude(rh => rh.User)
                .Include(b => b.BookCopies)
                    .ThenInclude(bc => bc.RentHistories)
                        .ThenInclude(rh => rh.IssuingAdmin)
                .Include(b => b.BookCopies)
                    .ThenInclude(bc => bc.RentHistories)
                        .ThenInclude(rh => rh.ReceivingAdmin)
                .ToListAsync();
        }


        public async Task<NormalBook?> GetBookWithLendingDetailsAsync(int bookId)
        {
            return await _context.NormalBooks
                .Include(b => b.BookCopies)
                    .ThenInclude(bc => bc.RentHistories)
                        .ThenInclude(rh => rh.User)
                .Include(b => b.BookCopies)
                    .ThenInclude(bc => bc.RentHistories)
                        .ThenInclude(rh => rh.IssuingAdmin)
                .Include(b => b.BookCopies)
                    .ThenInclude(bc => bc.RentHistories)
                        .ThenInclude(rh => rh.ReceivingAdmin)
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }

         // Get all book copies with RentHistory
    public async Task<List<BookCopy>> GetAllBookCopiesAsync()
    {
        return await _context.BookCopies
            .Include(bc => bc.Book)
            .Include(bc => bc.RentHistories)
            .ToListAsync();
    }

    // Get book copies by Book ID
    public async Task<List<BookCopy>> GetBookCopiesByBookIdAsync(int bookId)
    {
        return await _context.BookCopies
            .Where(bc => bc.BookId == bookId)
            .Include(bc => bc.Book)
            .Include(bc => bc.RentHistories)
            .ToListAsync();
    }


        public async Task<int> GetBorrowedCountInLastNDaysAsync(int userId, int days)
        {
            //var startDate = DateTime.UtcNow.AddDays(-days);
            //return await _context.RentHistory
            //    .Where(r => r.UserId == userId && r.LendDate >= startDate && r.ReturnDate == null)
            //    .CountAsync();
            var startDate = DateTime.UtcNow.AddDays(-days);
            return await _context.RentHistory
                .Where(r => r.UserId == userId && r.LendDate >= startDate )
                .CountAsync();
        }

        public async Task<int> GetBorrowedCountInDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            //return await _context.RentHistory
            //    .Where(r => r.UserId == userId && r.LendDate >= startDate && r.LendDate <= endDate && r.ReturnDate == null)
            //    .CountAsync();
            return await _context.RentHistory
                .Where(r => r.UserId == userId && r.LendDate >= startDate && r.LendDate <= endDate )
                .CountAsync();
        }


    }
}


