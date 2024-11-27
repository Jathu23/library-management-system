namespace library_management_system.Database.Entiy
{
    public class RentHistory
    {
        public int Id { get; set; }
        public int BookCopyId { get; set; }
        public int UserId { get; set; }
        public int IAdminId { get; set; }
        public int? RAdminId { get; set; }
        public DateTime LendDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }


        public BookCopy BookCopy { get; set; } 
        public User User { get; set; }
        public Admin IssuingAdmin { get; set; }
        public Admin? ReceivingAdmin { get; set; }

    }

}
