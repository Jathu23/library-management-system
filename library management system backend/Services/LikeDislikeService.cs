using library_management_system.Database;
using library_management_system.Database.Entiy;
using library_management_system.Repositories;

namespace library_management_system.Services
{
    public class LikeDislikeService
    {
        private readonly LikeDislikeRepository _likeDislikeRepository;

        public LikeDislikeService(LikeDislikeRepository likeDislikeRepository)
        {
            _likeDislikeRepository = likeDislikeRepository;
        }

      
    }

}
