using System.ComponentModel.DataAnnotations;

namespace library_management_system.Database.Entiy
{
    public class BookCopy
    {
        [Key]
        public int CopyId { get; set; }             
        public int BookId { get; set; }              
        public bool IsAvailable { get; set; }         
        public string Condition { get; set; }         
        public DateTime AddedDate { get; set; }      
        public DateTime? LastBorrowedDate { get; set; }
        public int lentcount { get; set; }

        // Navigation property
        public NormalBook Book { get; set; }
        public List<RentHistory> RentHistories { get; set; }
    }

}
