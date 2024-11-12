namespace library_management_system.DTOs.Admin
{
    public class AdminRequstModel
    {
        public string AdminNic { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public  IFormFile? ProfileImage { get; set; }
        public required string Password { get; set; }
    }
}
