namespace library_management_system.DTOs.LentRecord
{
    public class LendReportDto
    {
        public DateTime Date { get; set; }
        public int TotalRengings { get; set; }
        public int Pending {  get; set; }
        public int OnTime { get; set; }
        public int Later { get; set; }
        public List<LentHistoryAdminDto>? PendingLent {  get; set; }
        public List<LentHistoryAdminDto>? OnTimeLent {  get; set; }
        public List<LentHistoryAdminDto>? LaterLent {  get; set; }

    }
}
