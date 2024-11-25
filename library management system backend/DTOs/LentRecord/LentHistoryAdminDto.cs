namespace library_management_system.DTOs.LentRecord
{
    public class LentHistoryAdminDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public int AdminId { get; set; }
        public string AdminName { get; set; }

        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookISBN { get; set; }
        public string BookAuthor { get; set; }
        public string BookGenre { get; set; }
        public int BookPublishYear { get; set; }

        public int BookCopyId { get; set; }
        public string BookCondition { get; set; }
        public DateTime LentDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public string Status { get; set; }
        public int StatusValue { get; set; }
    }
}
