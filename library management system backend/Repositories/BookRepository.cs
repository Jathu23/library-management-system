using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;
using PdfSharp;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                    AddedDate = DateTime.Now,
                    lentcount = 0
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

        public async Task<(List<NormalBook>, int)> GetAllNormalBooksWithAvailableCopies(int page, int pageSize)
        {
            var query = _context.NormalBooks.Include(a => a.BookCopies).AsQueryable();

           
            var books = await query.OrderBy(b => b.Id) .
                                    Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            int totalRecords = await query.CountAsync();

            return (books, totalRecords);
        }


        public async Task<NormalBook?> GetBookWithCopies(int bookId,bool IdSelector)
        {
            try
            {
                if (IdSelector)
                {
                    // Include the BookCopies and filter for only available ones
                    return await _context.NormalBooks
                                         .Include(b => b.BookCopies)
                                         .FirstOrDefaultAsync(b => b.Id == bookId);
                }
                else
                {
                    var bookcopy = await _context.BookCopies.FindAsync(bookId);
                    return await _context.NormalBooks
                                        .Include(b => b.BookCopies)
                                        .FirstOrDefaultAsync(b => b.Id == bookcopy.BookId);
                }
            }
            catch
            {
                throw new Exception("The book or copy may have been deleted.");
            }
           
        }
        public async Task<(List<NormalBook>, int)> GetAllBooksWithCopies(int page, int pageSize)
        {
            var query = _context.NormalBooks.Include(a => a.BookCopies).AsQueryable();

            var books = await query.OrderBy(b => b.Id) 
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();

            int totalRecords = await query.CountAsync();

            return (books, totalRecords);

        }


        public async Task<(List<NormalBook>, int)> Categorization(string? genre, string? author, int? publishYear, int pageNumber, int pageSize)
        {
            var query = _context.NormalBooks.Include(b => b.BookCopies).AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre.Any(g => g.Contains(genre)));
            }
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.Contains(author));
            }
            if (publishYear.HasValue)
            {
                query = query.Where(b => b.PublishYear == publishYear.Value);
            }

            int totalRecords = await query.CountAsync();

            var books = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (books, totalRecords);
        }


      

            public async Task<(List<NormalBook>, int)> SearchAsync(string searchString, int pageNumber, int pageSize)
            {
                var query = _context.NormalBooks.AsQueryable();

                if (!string.IsNullOrEmpty(searchString))
                {
                    query = query.Where(b =>
                        b.Title.Contains(searchString) ||
                        b.Author.Contains(searchString) ||
                        b.Genre.Any(g => g.Contains(searchString)));
                }

                int totalRecords = await query.CountAsync();

                var books = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (books, totalRecords);
            }
        



    }
}
