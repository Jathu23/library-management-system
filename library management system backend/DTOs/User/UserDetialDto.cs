using library_management_system.DTOs.LentRecord;
using System.ComponentModel.DataAnnotations;

namespace library_management_system.DTOs.User
{
    public class UserDetialDto
    {
   
        public int Id { get; set; }
        public string? UserNic { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  string FullName { get; set; }
        public  string Email { get; set; }
        public  string PhoneNumber { get; set; }
        public  string Address { get; set; }
        public  string? ProfileImage { get; set; }
        public  DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsSubscribed { get; set; }
        public required BorrowStatusDto BorrowStatus { get; set; }
    }
}
