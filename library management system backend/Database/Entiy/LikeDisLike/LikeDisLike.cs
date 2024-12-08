namespace library_management_system.Database.Entiy.LikeDisLike
{
    public class LikeDisLike
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public bool IsLiked { get; set; }  // True for like, False for dislike
    }
}
