using library_management_system.DTOs.Ebook;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static library_management_system.DTOs.Ebook.UpdateEbookDto;

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
            var response = await _ebookService.AddNewEbook(ebookDto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("DeleteEbook")]
        public async Task<IActionResult> DeleteEbook(int id)
        {
            var response = await _ebookService.DeleteEbook(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateEbook(EbookUpdateDto ebookDto)
        {
            var response = await _ebookService.UpdateEbook(ebookDto);

            if (!response.Success)
            {
                return BadRequest(response); 
            }

            return Ok(response);  
        }

    }
}
