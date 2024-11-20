namespace library_management_system.DTOs.LoginPort
{
    public class LoginRequstDto
    {
        public required string  EmailOrNic { get; set; }
        public required string Password { get; set; }
    }
}
