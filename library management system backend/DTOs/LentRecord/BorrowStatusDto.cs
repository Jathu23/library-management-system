namespace library_management_system.DTOs.LentRecord
{
    public class BorrowStatusDto
    {
       
            public bool CanBorrow { get; set; }       // Indicates if the user can borrow more books
            public int BorrowLimit { get; set; }     // Borrow limit per cycle
            public int BooksBorrowed { get; set; }   // Number of books borrowed in the current cycle
            public string? Message { get; set; }     // Optional message (e.g., "Subscription period has ended")
        

    }

}
