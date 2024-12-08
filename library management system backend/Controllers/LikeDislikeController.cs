using library_management_system.Database;
using library_management_system.DTOs.LikeandReview;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeDislikeController : ControllerBase
    {
        private readonly LikeDislikeService _likeDislikeService;

        public LikeDislikeController(LikeDislikeService likeDislikeService)
        {
            _likeDislikeService = likeDislikeService;
        }


        // Like/Dislike Normal Book
        [HttpPost("normalbook")]
        public async Task<IActionResult> AddNormalBookLikeDislike([FromQuery] NormalBookLikeDislikeDto likeDislike)
        {
            var response = await _likeDislikeService.AddNormalBookLikeDislikeAsync(likeDislike);
            return Ok(response);
        }

        // Get Normal Book Like/Dislike count
        [HttpGet("normalbook-count-bookId")]
        public async Task<IActionResult> GetNormalBookLikeDislikeCount([FromQuery] int bookId, [FromQuery] bool isLiked)
        {
            var response = await _likeDislikeService.GetNormalBookLikeDislikeCountAsync(bookId, isLiked);
            return Ok(response);
        }

        // Add Ebook Like/Dislike
        [HttpPost("ebook")]
        public async Task<IActionResult> AddEbookLikeDislike([FromQuery] EbookLikeDislikeDto likeDislike)
        {
            var response = await _likeDislikeService.AddEbookLikeDislikeAsync(likeDislike);
            return Ok(response);
        }

        // Get Ebook Like/Dislike count
        [HttpGet("ebook-count-bookId")]
        public async Task<IActionResult> GetEbookLikeDislikeCount([FromQuery] int bookId, [FromQuery] bool isLiked)
        {
            var response = await _likeDislikeService.GetEbookLikeDislikeCountAsync(bookId, isLiked);
            return Ok(response);
        }

        // Add Audiobook Like/Dislike
        [HttpPost("audiobook")]
        public async Task<IActionResult> AddAudiobookLikeDislike([FromQuery] AudiobookLikeDislikeDto likeDislike)
        {
            var response = await _likeDislikeService.AddAudiobookLikeDislikeAsync(likeDislike);
            return Ok(response);
        }

        // Get Audiobook Like/Dislike count
        [HttpGet("audiobook-count-bookId")]
        public async Task<IActionResult> GetAudiobookLikeDislikeCount([FromQuery] int bookId, [FromQuery] bool isLiked)
        {
            var response = await _likeDislikeService.GetAudiobookLikeDislikeCountAsync(bookId, isLiked);
            return Ok(response);
        }

        public class NormalBookLikeDislikeDto
        {
            public int BookId { get; set; }  // The ID of the normal book
            public int UserId { get; set; }  // The ID of the user performing the like or dislike
            public bool IsLiked { get; set; }  // true for like, false for dislike
        }
        public class EbookLikeDislikeDto
        {
            public int BookId { get; set; }  // The ID of the ebook
            public int UserId { get; set; }  // The ID of the user performing the like or dislike
            public bool IsLiked { get; set; }  // true for like, false for dislike
        }
        public class AudiobookLikeDislikeDto
        {
            public int BookId { get; set; }  // The ID of the audiobook
            public int UserId { get; set; }  // The ID of the user performing the like or dislike
            public bool IsLiked { get; set; }  // true for like, false for dislike
        }

    }

}
