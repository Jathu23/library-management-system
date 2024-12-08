using System.ComponentModel.DataAnnotations;

namespace library_management_system.Database.Entiy.LikeDisLike
{
    public class EbookLikeDislike : LikeDisLike
    {
        [Key]
        public int Id { get; set; }

        // Navigation Property for User
        public User User { get; set; }
        public Ebook Ebook { get; set; }
    }
}
