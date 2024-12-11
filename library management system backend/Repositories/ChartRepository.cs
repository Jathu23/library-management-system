using library_management_system.Database;
using library_management_system.Database.Entiy;
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

        public async Task<List<RentHistory>> GetRentHistoryForYearAsync(int year)
        {
            return await _libraryDbContext.RentHistory
                .Where(r => r.LendDate.Year == year)
                .ToListAsync();
        }

        public async Task<int> GetMinYearAsync()
        {
            return await _libraryDbContext.RentHistory.MinAsync(r => r.LendDate.Year);
        }

        public async Task<int> GetMaxYearAsync()
        {
            return await _libraryDbContext.RentHistory.MaxAsync(r => r.LendDate.Year);
        }

        public async Task<List<RevenueData>> GetMonthlyRevenueAsync(int? Year)
        {
            int year = Year ?? DateTime.Now.Year;

            var monthlyRevenue = await _libraryDbContext.Payment
                .Where(p => p.PaymentDate.Year == year)
                .GroupBy(p => new { p.PaymentDate.Month, p.PaymentDate.Year })
                .Select(g => new RevenueData
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    TotalRevenue = g.Sum(p => p.AmountPaid)
                })
                .OrderBy(p => p.Month)
                .ToListAsync();

            return monthlyRevenue;
        }

        public class RevenueData
        {
            public int Month { get; set; }
            public int Year { get; set; }
            public decimal TotalRevenue { get; set; }
        }


    }

}
