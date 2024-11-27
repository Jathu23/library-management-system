using library_management_system.DTOs;
using library_management_system.DTOs.LentRecord;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LentController : ControllerBase
    {
        private readonly LentService _lentService;

        public LentController(LentService lentService)
        {
            _lentService = lentService;
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

        [HttpGet("lent-records-id")]
        public async Task<IActionResult> GetLentRecordForAdmin(int Userid)
        {
            var response = await _lentService.GetLentRecordForAdminAsync(Userid);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost("lend-by-copy-id")]
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


        [HttpGet("all-lent-All-records")]
        public async Task<IActionResult> GetAllLentRecords()
        {
            var result = await _lentService.GetAllLentRecordsAsync();
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("lent-historys")]
        public async Task<IActionResult> GetAllRentHistory(int page=1, int pageSize=5)
        {
            var result = await _lentService.GetAllRentHistory(page, pageSize);


            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


    }
}
