namespace library_management_system.Database.Entiy
{
    public class ForgotPasswordToken
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string TokenCode { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
