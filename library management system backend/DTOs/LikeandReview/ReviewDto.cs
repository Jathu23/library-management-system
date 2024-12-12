namespace library_management_system.DTOs.LikeandReview
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? UserProfil { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
