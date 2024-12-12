using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace library_management_system.Database.Entiy
{

    public class User
    {
        [Key]
        public  int Id { get; set; }
        public string? UserNic { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public required string? ProfileImage { get; set; }
        public required DateTime RegistrationDate { get; set; }
        public required bool IsActive { get; set; }
        public required bool IsSubscribed { get; set; }
  
    }



}
