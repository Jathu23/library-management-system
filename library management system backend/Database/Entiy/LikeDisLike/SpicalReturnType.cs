namespace library_management_system.Database.Entiy.LikeDisLike
{
    public class SpicalReturnType
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }  
        public bool IsLiked { get; set; }  
    }
}
