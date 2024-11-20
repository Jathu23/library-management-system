namespace library_management_system.DTOs.Book
{
    //public class NormalBookDto
    //{
    //    public int Id { get; set; }
    //    public string ISBN { get; set; }
    //    public string Title { get; set; }
    //    public string Author { get; set; }
    //    public List<string> Genre { get; set; }
    //    public int PublishYear { get; set; }
    //    public string ShelfLocation { get; set; }
    //    public int TotalCopies { get; set; }
    //    public int AvailableCopies { get; set; }
    //    public List<string> CoverImagePath { get; set; }
    //}


    public class NormalBookDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public List<string> Genre { get; set; }
        public int PublishYear { get; set; }
        public DateTime AddedDate { get; set; }
        public string ShelfLocation { get; set; }
        public int? RentCount { get; set; }
        public int TotalCopies { get; set; }
        public int AviableCopies { get; set; }
        public List<string> CoverImagePath { get; set; }
    }


}
