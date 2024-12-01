namespace library_management_system.DTOs.LentRecord
{

    public class LendingCountReportsDto
    {
        public DateTime Created { get; set; }
        public List<LendingCountReportDto> CountReports { get; set; }
    }
    public class LendingCountReportDto
    {
        public int BookID { get; set; }
        public int TotalRentCount { get; set; }
        public  List<InduvalCopyrentcount> induvalCopyrentcount { get; set; }


        public class InduvalCopyrentcount
        {
            public int CoppyId { get; set; }
            public int RentCount { get; set; }
        }
    }
}
