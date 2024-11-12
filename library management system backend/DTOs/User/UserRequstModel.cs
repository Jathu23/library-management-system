using System.Data;

namespace library_management_system.DTOs.User
{
    public class UserRequstModel
    {
        public string? UserNic { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public required string Password { get; set; } 
        public required IFormFile? ProfileImage { get; set; }
        public required bool IsActive { get; set; }
        public required bool IsSubscribed { get; set; }
    }
}
