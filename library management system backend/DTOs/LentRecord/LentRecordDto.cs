namespace library_management_system.DTOs.LentRecord
{
    public class LentRecordDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int AdminId { get; set; }
        public int DueDays { get; set; }
    }
}
