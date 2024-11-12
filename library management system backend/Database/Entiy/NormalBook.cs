namespace library_management_system.Database.Entiy
{
    public class NormalBook
    {
        public int Id { get; set; }                 
        public string ISBN { get; set; }             
        public string Title { get; set; }            
        public string Author { get; set; }          
        public string Genre { get; set; }            
        public int Copies { get; set; }              
        public int AvailableCopies { get; set; }     
        public int PublishYear { get; set; }         
        public DateTime AddedDate { get; set; }      
        public string ShelfLocation { get; set; }    
        public int RentCount { get; set; }           
        public string CoverImagePath { get; set; }   
    }

}
