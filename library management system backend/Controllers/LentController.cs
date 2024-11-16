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

            var response = await _lentService.LendNormalBookAsync(lentRecordDto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

    }
}
