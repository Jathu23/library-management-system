namespace library_management_system.DTOs.LentRecord
{
    //public class AllBookRendingReportDto
    public class BookLendingReportsDto
    {
        public DateTime Created { get; set; }
        public List<AllBookRendingReportDto> Reports { get; set; }
    }

    public class AllBookRendingReportDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public List<BookRentdetial> BookRentDetails { get; set; }

        public class BookRentdetial
        {
            public int BookCopyId { get; set; }
            public string UserName { get; set; }
            public string IssuingAdmin { get; set; }
            public string? ReceivingAdmin { get; set; }
            public DateTime LendDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? ReturnDate { get; set; }
        }
    }



}
