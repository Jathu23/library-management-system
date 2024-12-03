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
    }


}

