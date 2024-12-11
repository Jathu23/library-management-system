using library_management_system.DTOs;
using library_management_system.DTOs.LentRecord;
using library_management_system.Services;
using library_management_system.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LentController : ControllerBase
    {
        private readonly LentService _lentService;
        private readonly PdfGeneratorService _pdfGeneratorService;

        public LentController(LentService lentService, PdfGeneratorService pdfGeneratorService)
        {
            _lentService = lentService;
            _pdfGeneratorService = pdfGeneratorService;
        }

        [HttpPost("lend-normal-book")]
        public async Task<IActionResult> LendNormalBook( LentRecordDto lentRecordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request data.");

            var response = await _lentService.LendNormalBook(lentRecordDto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("lend-by-book-copyid")]
        public async Task<IActionResult> LendNormalBookByCopyId([FromQuery] LendByCopyIdDto lendByCopyIdDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Invalid input data",
                    Data = false
                });
            }

            var result = await _lentService.LendNormalBookByCopyId(lendByCopyIdDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("get-records-by-userid-admin")]
        public async Task<IActionResult> GetLentRecordForAdmin(int Userid)
        {
            var response = await _lentService.GetLentRecordForAdminAsync(Userid);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpGet("get-all-records-admin")]
        public async Task<IActionResult> GetAllLentRecords(bool? getoverdue = false)
        {
            var result = await _lentService.GetAllLentRecordsAsync(getoverdue);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpGet("lent-records-by-userid-user")]
        public async Task<IActionResult> GetLentRecordsByUserId(int userId , bool? getoverdue =false)
        {
            var result = await _lentService.GetLentRecordsByUserIdAsync(userId, getoverdue);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }



        [HttpGet("get-all-historys-admin")]
        public async Task<IActionResult> GetAllRentHistory(int page=1, int pageSize=5)
        {
            var result = await _lentService.GetAllRentHistory(page, pageSize);


            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

      


        [HttpGet("rent-history-by-userid")]
        public async Task<IActionResult> GetRentHistoryByUser(int userId)
        {
            var result = await _lentService.GetRentHistoryByUser(userId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpGet("Lent-Report")]
        public async Task<IActionResult> GetLentReport([FromQuery] DateTime date)
        {
            try
            {
                var report = await _lentService.GetLentReport(date);
                var pdf =await _pdfGeneratorService.GenerateLendReportPdfAsync(report);
               
           
                return File(pdf, "application/pdf", "LendReport.pdf");
                return Ok(pdf);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpGet("Lent-Report-ByUserid")]
        public async Task<IActionResult> GetLentReportbyuserid([FromQuery] int userid)
        {
            try
            {
                var report = await _lentService.GetLentReportbyuserid(userid);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }


        [HttpGet("Book-lending-report")]
        public async Task<IActionResult> GetBookLendingReports([FromQuery] int? bookId)
        {
            try
            {
                var reports = await _lentService.GetBookLendingReports(bookId);
                return Ok(reports);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("download-lending-report")]
        public async Task<IActionResult> DownloadLendingReport([FromQuery] int? bookId)
        {
            var report = await _lentService.GetBookLendingReports(bookId); // Fetch your data
            var pdfBytes = await _pdfGeneratorService.GenerateBookLendingReportPdfAsync(report);

            return File(pdfBytes, "application/pdf", "BookLendingReport.pdf");
        }

        [HttpGet("LendingCount-report")]
        public async Task<IActionResult> GetLendingCountReport([FromQuery] int? bookId)
        {
            var report = await _lentService.GetLendingCountReportsAsync(bookId);
            return Ok(report);
        }


        [HttpGet("can-borrow")]
        public async Task<IActionResult> CanUserBorrow(int userId)
        {
            var borrowStatus = await _lentService.CanUserBorrowBooks(userId);

            if (borrowStatus == null)
            {
                return NotFound(new { Message = "User not found or subscription details are missing." });
            }

            if (!borrowStatus.CanBorrow)
            {
                return BadRequest(new { Message = borrowStatus.Message });
            }

            return Ok(borrowStatus);
        }

        [HttpGet("lend-report")]
        public async Task<IActionResult> GetLendReport()
        {
            // Sample HTML content for the report
            var htmlContent = @"
            <html>
            <head><style>body { font-family: Arial, sans-serif; }</style></head>
            <body>
                <h1>Lend Report</h1>
                <p>Date: " + DateTime.Now + @"</p>
                <p>Total Rentings: 20</p>
                <p>Pending: 5</p>
                <p>On Time: 10</p>
                <p>Later: 5</p>
                <h3>Details</h3>
                <ul>
                    <li>Pending Record 1</li>
                    <li>Pending Record 2</li>
                </ul>
            </body>
            </html>";

            // Generate the PDF
            var pdfBytes = await _pdfGeneratorService.GeneratePdfAsync(htmlContent);

            // Return as a downloadable file
            return File(pdfBytes, "application/pdf", "LendReport.pdf");
        }



    }
}
