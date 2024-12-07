using library_management_system.DTOs.LikeandReview;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdateReview([FromQuery] ReviewRequest model)
        {
            if (model == null || model.UserId <= 0 || model.BookId <= 0 || model.Rating < 1 || model.Rating > 5)
                return BadRequest("Invalid data.");

            try
            {
                await _reviewService.AddOrUpdateReviewAsync(model.UserId, model.BookId, model.BookType, model.ReviewText, model.Rating);
                return Ok("Review recorded successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByBook")]
        public async Task<IActionResult> GetReviewsByBook(int bookId, string bookType)
        {
            var reviews = await _reviewService.GetReviewsByBookAsync(bookId, bookType);

            if (reviews == null || reviews.Count == 0)
                return NotFound("No reviews found for this book.");

            return Ok(reviews);
        }

        [HttpGet("GetByUserAndBook")]
        public async Task<IActionResult> GetReviewByUserAndBook(int userId, int bookId, string bookType)
        {
            var review = await _reviewService.GetReviewByUserAndBookAsync(userId, bookId, bookType);

            if (review == null)
                return NotFound("No review found for this user and book.");

            return Ok(review);
        }
    }
}
