namespace library_management_system.DTOs.LentRecord
{
    public class LentRecordUserDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }

        public int BookCopyId { get; set; }

        public string BookAuthor { get; set; }
        public DateTime LentDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public int StatusValue { get; set; }
    }
}
