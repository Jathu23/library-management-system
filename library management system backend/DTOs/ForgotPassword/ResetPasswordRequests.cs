namespace library_management_system.DTOs.ForgotPassword
{
    public class ResetPasswordRequests
    {
        public string Email { get; set; }
        public string OtpCode { get; set; } 
        public string NewPassword { get; set; }
    }
}
