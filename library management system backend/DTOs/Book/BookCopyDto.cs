namespace library_management_system.DTOs.Book
{
    public class BookCopyDto
    {
        public int CopyId { get; set; }
        public string Condition { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? LastBorrowedDate { get; set; }
    }

}
