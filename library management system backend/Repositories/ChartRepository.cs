using library_management_system.Database;
using library_management_system.Database.Entiy;
using library_management_system.DTOs.Chart;
using Microsoft.EntityFrameworkCore;
using static library_management_system.Repositories.ChartRepository;

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

        public async Task<List<RevenueData>> GetMonthlyRevenueAsync(int? year)
        {
            int targetYear = year ?? DateTime.Now.Year;

            return await _libraryDbContext.Payment
                .Where(p => p.PaymentDate.Year == targetYear)
                .GroupBy(p => new { p.PaymentDate.Month })
                .Select(g => new RevenueData
                {
                    Month = g.Key.Month,
                    TotalRevenue = g.Sum(p => p.AmountPaid)
                })
                .OrderBy(r => r.Month)
                .ToListAsync();
        }

        public class RevenueData
        {
            public int Month { get; set; } // Changed to int to store month number
            public decimal TotalRevenue { get; set; }
        }


    }

}
