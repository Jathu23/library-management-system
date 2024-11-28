namespace library_management_system.DTOs.User
{
    public class usersearchDTO
    {
       
            public int Id { get; set; }
            public string? UserNic { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public IFormFile? ProfileImage { get; set; }
            public bool? IsActive { get; set; }
            public bool IsSubscribed { get; set; }
        
    }
}
