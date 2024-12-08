using library_management_system.Database;
using library_management_system.Database.Entiy.ReviewEntitys;
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


        // Add Normal Book Review
        [HttpPost("normal-book/review")]
        public async Task<IActionResult> AddNormalBookReview([FromQuery] NormalBookReviewRequest reviewRequest)
        {
            var review = new NormalBookReview
            {
                UserId = reviewRequest.UserId,
                BookId = reviewRequest.BookId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating,
                ReviewDate = DateTime.UtcNow
            };

            var result = await _reviewService.AddNormalBookReviewAsync(review);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // Get Reviews for Normal Book
        [HttpGet("normal-book-reviews")]
        public async Task<IActionResult> GetNormalBookReviews(int bookId)
        {
            var result = await _reviewService.GetNormalBookReviewsAsync(bookId);
            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // Add Ebook Review
        [HttpPost("ebook-review")]
        public async Task<IActionResult> AddEbookReview([FromQuery] EbookReviewRequest reviewRequest)
        {
            var review = new EbookReview
            {
                UserId = reviewRequest.UserId,
                BookId = reviewRequest.BookId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating,
                ReviewDate = DateTime.UtcNow
            };

            var result = await _reviewService.AddEbookReviewAsync(review);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // Get Reviews for Ebook
        [HttpGet("ebook-reviews")]
        public async Task<IActionResult> GetEbookReviews(int bookId)
        {
            var result = await _reviewService.GetEbookReviewsAsync(bookId);
            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // Add Audiobook Review
        [HttpPost("audiobook-review")]
        public async Task<IActionResult> AddAudiobookReview([FromQuery] AudiobookReviewRequest reviewRequest)
        {
            var review = new AudiobookReview
            {
                UserId = reviewRequest.UserId,
                BookId = reviewRequest.BookId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating,
                ReviewDate = DateTime.UtcNow
            };

            var result = await _reviewService.AddAudiobookReviewAsync(review);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // Get Reviews for Audiobook
        [HttpGet("audiobook-reviews")]
        public async Task<IActionResult> GetAudiobookReviews(int bookId)
        {
            var result = await _reviewService.GetAudiobookReviewsAsync(bookId);
            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        public class NormalBookReviewRequest
        {
            public int UserId { get; set; }
            public int BookId { get; set; }
            public string ReviewText { get; set; }
            public int Rating { get; set; }
        }

        public class EbookReviewRequest
        {
            public int UserId { get; set; }
            public int BookId { get; set; }
            public string ReviewText { get; set; }
            public int Rating { get; set; }
        }
        public class AudiobookReviewRequest
        {
            public int UserId { get; set; }
            public int BookId { get; set; }
            public string ReviewText { get; set; }
            public int Rating { get; set; }
        }
    }

}
