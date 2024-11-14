using library_management_system.DTOs.Ebook;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EbookController : ControllerBase
    {
        private readonly EbookService _ebookService;

        public EbookController(EbookService ebookService)
        {
            _ebookService = ebookService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEbook([FromForm] AddEbookDto ebookDto)
        {
            var response = await _ebookService.AddNewEbookAsync(ebookDto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
