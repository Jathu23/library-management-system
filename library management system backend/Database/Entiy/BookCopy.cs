using System.ComponentModel.DataAnnotations;

namespace library_management_system.Database.Entiy
{
    public class BookCopy
    {
        [Key]
        public int CopyId { get; set; }             
        public int BookId { get; set; }               // Foreign key linking to the NormalBook
        public bool IsAvailable { get; set; }         // Whether the copy is currently available
        public string Condition { get; set; }         // Condition of the copy (e.g., new, worn)
        public DateTime AddedDate { get; set; }       // Date this copy was added to the library
        public DateTime? LastBorrowedDate { get; set; } // Date the copy was last borrowed

        // Navigation property
        public NormalBook Book { get; set; }          // Reference to the main book information
    }

}
