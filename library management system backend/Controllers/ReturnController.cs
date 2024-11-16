using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnController : ControllerBase
    {
        private readonly ReturnService _returnService;

        public ReturnController(ReturnService returnService)
        {
            _returnService = returnService;
        }

        [HttpPost("return-lent-book")]
        public async Task<IActionResult> ReturnLentBook(int lentRecordId)
        {
            if (lentRecordId <= 0)
                return BadRequest("Invalid lent record ID.");

            var response = await _returnService.ReturnLentBook(lentRecordId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

    }
}
