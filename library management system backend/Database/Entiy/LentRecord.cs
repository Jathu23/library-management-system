namespace library_management_system.Database.Entiy
{
    public class LentRecord
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookCopyId { get; set; }
        public int AdminId { get; set; }
        public DateTime LentDate { get; set; }
        public DateTime DueDate { get; set; }
        public BookCopy BookCopy { get; set; } // Optional, but useful for accessing book copy details
        public User User { get; set; }
        public Admin Admin { get; set; }
    }


}
