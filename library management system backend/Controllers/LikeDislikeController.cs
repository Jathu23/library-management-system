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

        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdateLikeDislike([FromQuery] LikeDislikeRequest model)
        {
            if (model == null || model.UserId <= 0 || model.BookId <= 0)
                return BadRequest("Invalid data.");

            try
            {
                await _likeDislikeService.AddOrUpdateLikeDislikeAsync(model.UserId, model.BookId, model.BookType, model.IsLike);
                return Ok("Like/Dislike action recorded successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByUserAndBook")]
        public async Task<IActionResult> GetLikeDislike(int userId, int bookId, string bookType)
        {
            var likeDislike = await _likeDislikeService.GetLikeDislikeAsync(userId, bookId, bookType);

            if (likeDislike == null)
                return NotFound("No like/dislike found for this user and book.");

            return Ok(likeDislike);
        }
    }
}
