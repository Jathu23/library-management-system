namespace library_management_system.Database.Entiy
{
    public class LoginT
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public string? NIC { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; }
        public required int MemberId { get; set; }
       

            
    }
}
