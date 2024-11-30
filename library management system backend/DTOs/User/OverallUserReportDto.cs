namespace library_management_system.DTOs.User
{
    public class OverallUserReportDto
    {
        public DateTime? Created { get; set; }
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int NonActiveUsers { get; set; }
        public int SubcribeUsers { get; set; }

    }
}
