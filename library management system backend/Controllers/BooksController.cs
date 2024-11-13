using library_management_system.DTOs.Book;
using library_management_system.DTOs;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
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
        public async Task<IActionResult> GetAllNormalBooksWithAvailableCopies()
        {
            var response = await _bookService.GetAllNormalBooksWithAvailableCopies();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("get-book")]
        public async Task<IActionResult> GetBookWithAvailableCopies(int bookId)
        {
            var response = await _bookService.GetBookWithAvailableCopies(bookId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("get-all-books-with-copies")]
        public async Task<IActionResult> GetAllBooksWithCopies()
        {
            var response = await _bookService.GetAllBooksWithCopies();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }


    }
}
