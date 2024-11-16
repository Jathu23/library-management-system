namespace library_management_system.Database.Entiy
{
    public class LentRecord
    {
        public int Id { get; set; }
        public int BookCopyId { get; set; }
        public int UserId { get; set; }
        public int AdminId { get; set; } 
        public DateTime LendDate { get; set; }
        public DateTime DueDate { get; set; }
    }

}
