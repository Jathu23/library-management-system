using library_management_system.Database;
using library_management_system.DTOs.Chart;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly ChartService _chartService;

        public ChartController(ChartService chartService)
        {
            _chartService = chartService;
        }
        [HttpGet("borrowing-trends")]
        public IActionResult GetBorrowingTrends()
        {
            var data = _chartService.GetBorrowingTrends();
            return Ok(data);
        }

        [HttpGet("all-borrowing-trends")]
        public async Task<IActionResult> GetBorrowingTrendsForAllYears()
        {
            var data = await _chartService.GetBorrowingTrendsForAllYears();
            return Ok(data);
        }


        [HttpGet("monthly-revenue")]
        public async Task<IActionResult> GetMonthlyRevenue(int? year)
        {
            var revenueData = await _chartService.GetMonthlyRevenueForChartAsync(year);
            return Ok(revenueData);
        }
    }


}

