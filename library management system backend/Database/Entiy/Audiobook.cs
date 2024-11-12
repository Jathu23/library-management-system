namespace library_management_system.Database.Entiy
{
    public class Audiobook
    {
        public int Id { get; set; }                 
        public string ISBN { get; set; }            
        public string Title { get; set; }           
        public string Author { get; set; }           
        public string Genre { get; set; }           
        public int PublishYear { get; set; }         
        public DateTime AddedDate { get; set; }      
        public string Narrator { get; set; }        
        public TimeSpan Duration { get; set; }       
        public string AudioFormat { get; set; }     
        public string FilePath { get; set; }         
        public double FileSize { get; set; }         
        public string SampleAudioPath { get; set; } 
        public string CoverImagePath { get; set; }   
    }

}
