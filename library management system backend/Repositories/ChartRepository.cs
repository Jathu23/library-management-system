using library_management_system.Database;
using library_management_system.DTOs.Chart;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class ChartRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public ChartRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }


        //public List<ChartData> GetBorrowingTrends()
        //{
        //    int currentYear = DateTime.Now.Year;

        //    // Fetch and group data by month for the current year
        //    var data = _libraryDbContext.RentHistory
        //        .Where(r => r.LendDate.Year == currentYear) // Filter by the current year
        //        .GroupBy(r => r.LendDate.Month) // Group by month
        //        .Select(group => new
        //        {
        //            Month = group.Key,
        //            BorrowCount = group.Count() // Count the number of records per month
        //        })
        //        .ToList()
        //        .OrderBy(g => g.Month) // Order by month
        //        .Select(g => new ChartSeries
        //        {
        //            Name = new DateTime(currentYear, g.Month, 1).ToString("MMMM"), // Convert month number to name
        //            Value = g.BorrowCount
        //        })
        //        .ToList();

        //    // Wrap the data in ChartData format
        //    return new List<ChartData>
        //{
        //    new ChartData
        //    {
        //        Name = "Books Borrowed",
        //        Series = data
        //    }
        //};
        //}

        public List<ChartSeries> GetBorrowingTrends()
        {
            int currentYear = DateTime.Now.Year;

            return _libraryDbContext.RentHistory
                .Where(r => r.LendDate.Year == currentYear)
                .GroupBy(r => r.LendDate.Month)
                .Select(group => new ChartSeries
                {
                    Name = new DateTime(currentYear, group.Key, 1).ToString("MMMM"),
                    Value = group.Count()
                })
                .ToList();
        }

    }

}
