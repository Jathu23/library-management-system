﻿using library_management_system.DTOs.Chart;
using library_management_system.Repositories;
using System.Globalization;

namespace library_management_system.Services
{
    public class ChartService
    {
        public readonly ChartRepository _chartRepository;

        public ChartService(ChartRepository repository)
        {
            this._chartRepository = repository;
        }
        public List<ChartData> GetBorrowingTrends()
        {
            int currentYear = DateTime.Now.Year;

            // Fetch raw data from repository
            var rawData = _chartRepository.GetBorrowingTrends();

            // Generate a complete list of months
            var allMonths = Enumerable.Range(1, 12)
                .Select(month => new ChartSeries
                {
                    Name = new DateTime(currentYear, month, 1).ToString("MMMM"),
                    Value = 0 // Default to zero
                })
                .ToList();

            // Merge raw data with all months
            var mergedData = allMonths
                .Select(month => new ChartSeries
                {
                    Name = month.Name,
                    Value = rawData.FirstOrDefault(d => d.Name == month.Name)?.Value ?? 0 // Use raw data if available, else 0
                })
                .ToList();

            // Wrap merged data in ChartData
            return new List<ChartData>
        {
            new ChartData
            {
                Name = "Books Borrowed",
                Series = mergedData
            }
        };
        }


        public async Task<List<ChartData>> GetBorrowingTrendsForAllYears()
        {
            var minYear = await _chartRepository.GetMinYearAsync();
            var maxYear = await _chartRepository.GetMaxYearAsync();
            var years = Enumerable.Range(minYear, maxYear - minYear + 1);

            var result = new List<ChartData>();

            foreach (var year in years)
            {
                var rentHistory = await _chartRepository.GetRentHistoryForYearAsync(year);
                var monthlyData = rentHistory
                    .GroupBy(r => r.LendDate.Month)
                    .Select(g => new { Month = g.Key, BorrowCount = g.Count() })
                    .ToList();

                var series = Enumerable.Range(1, 12)
                    .Select(month => new ChartSeries
                    {
                        Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                        Value = monthlyData.FirstOrDefault(d => d.Month == month)?.BorrowCount ?? 0
                    })
                    .ToList();

                result.Add(new ChartData
                {
                    Name = $"year {year}",
                    Series = series
                });
            }

            return result;
        }

        public async Task<List<object>> GetMonthlyRevenueForChartAsync(int? year)
        {
            // Fetch revenue data for the given year
            var revenueData = await _chartRepository.GetMonthlyRevenueAsync(year);

            // Get the list of all month names in the current culture
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Take(12).ToList();

            // Map each month to its corresponding revenue or a default value of 0
            var result = months.Select((month, index) =>
            {
                var dataForMonth = revenueData.FirstOrDefault(r => r.Month == index + 1); // Match month by index
                return new
                {
                    name = month, // Month name (e.g., "January")
                    value = (int)(dataForMonth?.TotalRevenue ?? 0) // Total revenue or 0 if no data
                };
            }).ToList();

            // Return the result as a list of objects
            return result.Cast<object>().ToList();
        }


    }
}
