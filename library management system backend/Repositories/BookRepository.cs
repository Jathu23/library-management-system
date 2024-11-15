﻿using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class BookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewBookWithCopies(NormalBook book, int copyCount)
        {
          
          await  _context.NormalBooks.AddAsync(book);
            await _context.SaveChangesAsync();

            
            for (int i = 0; i < copyCount; i++)
            {
                var bookCopy = new BookCopy
                {
                    BookId = book.Id,             
                    IsAvailable = true,
                    Condition = "New",
                    AddedDate = DateTime.Now
                };
                await _context.BookCopies.AddAsync(bookCopy);
            }

            await _context.SaveChangesAsync();

            return book.Id; 
        }

        public async Task<NormalBook> GetBookById(int id)
        {
            return await _context.NormalBooks.Include(b => b.BookCopies).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<int> UpdateNormalBook(NormalBook book)
        {
            _context.NormalBooks.Update(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task AddBookCopies(List<BookCopy> copies)
        {
            await _context.BookCopies.AddRangeAsync(copies);
            await _context.SaveChangesAsync();
        }


        public async Task<BookCopy> GetBookCopyById(int copyId)
        {
            return await _context.BookCopies.FirstOrDefaultAsync(bc => bc.CopyId == copyId);
        }

        public async Task DeleteBookCopyAsync(BookCopy bookCopy)
        {
            _context.BookCopies.Remove(bookCopy);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllBookCopiesByBookId(int bookId)
        {
            var bookCopies = await _context.BookCopies.Where(bc => bc.BookId == bookId).ToListAsync();
            _context.BookCopies.RemoveRange(bookCopies);
            await _context.SaveChangesAsync();
        }

      
        public async Task DeleteNormalBook(NormalBook book)
        {
            _context.NormalBooks.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<List<NormalBook>> GetAllNormalBooksWithAvailableCopies()
        {
            // Include the BookCopies and filter the ones that are available
            return await _context.NormalBooks
                                 .Include(b => b.BookCopies) // Include associated book copies
                                 .ToListAsync();
        }

        public async Task<NormalBook> GetBookWithCopies(int bookId)
        {
            // Include the BookCopies and filter for only available ones
            return await _context.NormalBooks
                                 .Include(b => b.BookCopies)
                                 .FirstOrDefaultAsync(b => b.Id == bookId);
        }
        public async Task<List<NormalBook>> GetAllBooksWithCopies()
        {
            // Include the BookCopies navigation property to load related copies for each book
            return await _context.NormalBooks
                                 .Include(b => b.BookCopies)
                                 .ToListAsync();
        }


    }
}
