using library_management_system.DTOs.Book;
using library_management_system.DTOs;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure;
using library_management_system.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly BookRepository _bookRepository;

        public BooksController(BookService bookService, BookRepository bookRepository)
        {
            _bookService = bookService;
            _bookRepository = bookRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBook( AddBookDto bookDto)
        {
            var response = await _bookService.AddNewBook(bookDto);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateBook(UpdateBookDto bookDto)
        {
          
            var response = await _bookService.UpdateNormalBook(bookDto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("add-copies")]
        public async Task<IActionResult> AddCopies(int bookId, int numberOfCopies)
        {
           
            if (numberOfCopies <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "An error occurred while adding copies",
                    Errors = new List<string> { "Number of copies must be greater than zero." }
                });
            }

          
            var response = await _bookService.AddCopiesToBook(bookId, numberOfCopies);

            
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("delete-copy")]
        public async Task<IActionResult> DeleteBookCopy(int copyId)
        {
            if (copyId <= 0)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "An error occurred while deleting the book copy",
                    Errors = new List<string> { "CopyId must be greater than zero." }
                });
            }
             

            var response = await _bookService.DeleteBookCopyByCopyId(copyId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("delete-book")]
        public async Task<IActionResult> DeleteNormalBookWithCopies(int bookId)
        {
            if (bookId <= 0)
            {

                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "An error occurred while deleting the book ",
                    Errors = new List<string> { "bookId must be greater than zero." }
                });
            }
                

            var response = await _bookService.DeleteNormalBook(bookId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpGet("get-all-books")]
        public async Task<IActionResult> GetAllNormalBooksWithAvailableCopies(int page, int pageSize)
        {
            var response = await _bookService.GetAllNormalBooksWithAvailableCopies(page,pageSize);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("get-book")]
        public async Task<IActionResult> GetBookWithAvailableCopies(int bookId , bool IdSelector = false)
        {
            var response = await _bookService.GetBookWithAvailableCopies(bookId, IdSelector);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("get-all-books-with-copies")]
        public async Task<IActionResult> GetAllBooksWithCopies(int page, int pageSize)
        {
            var response = await _bookService.GetAllBooksWithCopies(page, pageSize);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("Categorize")]
        public async Task<IActionResult> Categorize(
            [FromQuery] string? genre,
            [FromQuery] string? author,
            [FromQuery] int? publishYear,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _bookService.GetCategorizedBooks(genre, author, publishYear, pageNumber, pageSize);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Categorize(
            [FromQuery] string? searchstring,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _bookService.GetSearchBooks(searchstring, pageNumber, pageSize);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }



        // API to get distinct genres, authors, and publish years
        [HttpGet("get-distinct-attributes")]
        public async Task<IActionResult> GetDistinctAttributes()
        {
            var result = await _bookService.GetDistinctBookAttributesAsync();
            return Ok(result);
        }

        [HttpPost ("sample")]
        public async Task<int> addsample()
        {
            var count = 0;
            var books = new List<AddBookDto>
{
    new AddBookDto { ISBN = "978-1-234567-89-0", Title = "The Adventure Begins", Author = "John Doe", Genre = new List<string> { "Adventure", "Action" }, PublishYear = 2020, ShelfLocation = "A1", TotalCopies = 5, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-89-1", Title = "Science Unveiled", Author = "Jane Smith", Genre = new List<string> { "Science", "Non-fiction" }, PublishYear = 2019, ShelfLocation = "B2", TotalCopies = 3, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-89-2", Title = "The Art of Cooking", Author = "Emma Brown", Genre = new List<string> { "Cooking", "Lifestyle" }, PublishYear = 2021, ShelfLocation = "C3", TotalCopies = 7, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-89-3", Title = "Fantasy World", Author = "Alice Johnson", Genre = new List<string> { "Fantasy", "Adventure" }, PublishYear = 2020, ShelfLocation = "D4", TotalCopies = 10, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-89-4", Title = "Learning Programming", Author = "David Lee", Genre = new List<string> { "Technology", "Programming" }, PublishYear = 2022, ShelfLocation = "E5", TotalCopies = 6, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-89-5", Title = "Mystery of the Forest", Author = "Sophia White", Genre = new List<string> { "Mystery", "Thriller" }, PublishYear = 2018, ShelfLocation = "F6", TotalCopies = 8, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-89-6", Title = "History Rewritten", Author = "Michael Black", Genre = new List<string> { "History", "Non-fiction" }, PublishYear = 2017, ShelfLocation = "G7", TotalCopies = 4, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-89-7", Title = "The Last Kingdom", Author = "Olivia Green", Genre = new List<string> { "Fantasy", "Action" }, PublishYear = 2021, ShelfLocation = "H8", TotalCopies = 5, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-89-8", Title = "Introduction to AI", Author = "Daniel Harris", Genre = new List<string> { "Technology", "Artificial Intelligence" }, PublishYear = 2022, ShelfLocation = "I9", TotalCopies = 3, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-89-9", Title = "The Power of Habit", Author = "Chris White", Genre = new List<string> { "Self-help", "Psychology" }, PublishYear = 2019, ShelfLocation = "J10", TotalCopies = 9, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-90-0", Title = "Space Exploration", Author = "George Brown", Genre = new List<string> { "Science", "Space" }, PublishYear = 2020, ShelfLocation = "K11", TotalCopies = 4, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-90-1", Title = "The Silent Sea", Author = "Lily Clarke", Genre = new List<string> { "Science Fiction", "Adventure" }, PublishYear = 2021, ShelfLocation = "L12", TotalCopies = 7, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-90-2", Title = "Mindfulness in Daily Life", Author = "James Young", Genre = new List<string> { "Self-help", "Wellness" }, PublishYear = 2018, ShelfLocation = "M13", TotalCopies = 5, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-90-3", Title = "The Secrets of Nature", Author = "Rebecca King", Genre = new List<string> { "Nature", "Science" }, PublishYear = 2017, ShelfLocation = "N14", TotalCopies = 6, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-90-4", Title = "Creative Writing", Author = "William Harris", Genre = new List<string> { "Writing", "Education" }, PublishYear = 2022, ShelfLocation = "O15", TotalCopies = 8, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-90-5", Title = "The Psychology of Success", Author = "Laura Scott", Genre = new List<string> { "Self-help", "Psychology" }, PublishYear = 2020, ShelfLocation = "P16", TotalCopies = 5, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-90-6", Title = "The Quantum World", Author = "Brian Lewis", Genre = new List<string> { "Science", "Physics" }, PublishYear = 2021, ShelfLocation = "Q17", TotalCopies = 4, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-90-7", Title = "The Business of Life", Author = "Emily White", Genre = new List<string> { "Business", "Self-help" }, PublishYear = 2019, ShelfLocation = "R18", TotalCopies = 6, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-90-8", Title = "The Digital Revolution", Author = "Jack Walker", Genre = new List<string> { "Technology", "Business" }, PublishYear = 2022, ShelfLocation = "S19", TotalCopies = 5, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-90-9", Title = "The Evolution of Language", Author = "Paul Turner", Genre = new List<string> { "Linguistics", "Philosophy" }, PublishYear = 2018, ShelfLocation = "T20", TotalCopies = 7, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-91-0", Title = "Machine Learning Simplified", Author = "Sandra Lee", Genre = new List<string> { "Technology", "Machine Learning" }, PublishYear = 2021, ShelfLocation = "U21", TotalCopies = 8, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-91-1", Title = "The Smart Investor", Author = "Joseph Clark", Genre = new List<string> { "Business", "Finance" }, PublishYear = 2019, ShelfLocation = "V22", TotalCopies = 6, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-91-2", Title = "Healthy Eating", Author = "Alice Wilson", Genre = new List<string> { "Health", "Lifestyle" }, PublishYear = 2022, ShelfLocation = "W23", TotalCopies = 7, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-91-3", Title = "Global Politics", Author = "David Thomas", Genre = new List<string> { "Politics", "Social Science" }, PublishYear = 2021, ShelfLocation = "X24", TotalCopies = 4, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-91-4", Title = "Leadership for Beginners", Author = "Karen Lewis", Genre = new List<string> { "Business", "Leadership" }, PublishYear = 2020, ShelfLocation = "Y25", TotalCopies = 9, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-91-5", Title = "Digital Transformation", Author = "Mark Turner", Genre = new List<string> { "Technology", "Business" }, PublishYear = 2022, ShelfLocation = "Z26", TotalCopies = 6, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-91-6", Title = "Understanding Economics", Author = "Julia Davis", Genre = new List<string> { "Economics", "Non-fiction" }, PublishYear = 2021, ShelfLocation = "A27", TotalCopies = 8, CoverImages = null },
    new AddBookDto { ISBN = "978-1-234567-91-7", Title = "Secrets of a Great Leader", Author = "Nathan Carter", Genre = new List<string> { "Business", "Leadership" }, PublishYear = 2022, ShelfLocation = "B28", TotalCopies = 7, CoverImages = null }
};


            foreach (var item in books)
            {
                var response = await _bookService.AddNewBook(item);
                count += response.Data;
                Thread.Sleep(200);
            }


            return count;

        }

        [HttpPost("bookimageupdate")]
        public async Task<int> updatebookimage()
        {
           
              return await _bookRepository.UpdateCustomData();
        }
    }
}
