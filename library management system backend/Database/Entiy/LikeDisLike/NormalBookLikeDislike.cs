using System.ComponentModel.DataAnnotations;

namespace library_management_system.Database.Entiy.LikeDisLike
{
    public class NormalBookLikeDislike : LikeDisLike
    {
        [Key]
        public int Id { get; set; }

        // Navigation Property for User
        public User User { get; set; }
        public NormalBook NormalBook { get; set; }
    }
}
