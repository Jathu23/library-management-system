namespace library_management_system.Database.Entiy
{
    public class NormalBook
    {
        public int Id { get; set; }               
        public string ISBN { get; set; }           
        public string Title { get; set; }       
        public string Author { get; set; }          
        public List<string> Genre { get; set; }          
        public int PublishYear { get; set; }        
        public DateTime AddedDate { get; set; }     
        public string ShelfLocation { get; set; }    
        public int RentCount { get; set; }          
        public int TotalCopies { get; set; }          
        public List<string> CoverImagePath { get; set; }  


        // Navigation property for book copies
        public List<BookCopy> BookCopies { get; set; } 

        //public List<LentRecord> lentRecords { get; set; }
    }


}
