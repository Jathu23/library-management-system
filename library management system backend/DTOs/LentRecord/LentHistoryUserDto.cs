namespace library_management_system.DTOs.LentRecord
{
    public class LentHistoryUserDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }

        public int BookCopyId { get; set; }
        public DateTime LentDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
        public int StatusValue { get; set; }
    }
}
