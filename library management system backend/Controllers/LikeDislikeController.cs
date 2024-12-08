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

      
    }

}
