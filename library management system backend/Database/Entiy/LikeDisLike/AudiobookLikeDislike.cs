using System.ComponentModel.DataAnnotations;

namespace library_management_system.Database.Entiy.LikeDisLike
{
    public class AudiobookLikeDislike : LikeDisLike
    {
        [Key]
        public int Id { get; set; }

        // Navigation Property for User
        public User User { get; set; }
        public Audiobook Audiobook { get; set; }
    }
}
