namespace library_management_system.DTOs.Chart
{
    public class ChartData
    {
        public string Name { get; set; } // Example: "Books Borrowed"
        public List<ChartSeries> Series { get; set; }
    }

    public class ChartSeries
    {
        public string Name { get; set; } // Example: "January"
        public int Value { get; set; }   // Example: 50 (books borrowed)
    }
}
