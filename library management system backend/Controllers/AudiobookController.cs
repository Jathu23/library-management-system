using library_management_system.DTOs.AudioBook;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudiobookController : ControllerBase
    {
        private readonly AudioBookService _audioBookService;

        public AudiobookController(AudioBookService audioBookService)
        {
            _audioBookService = audioBookService;
        }

        [HttpPost("add-audiobook")]
        public async Task<IActionResult> AddAudiobook([FromForm] AddAudiobookDto audiobookDto)
        {
            var response = await _audioBookService.AddAudiobook(audiobookDto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("update-audiobook")]
        public async Task<IActionResult> UpdateAudiobook([FromForm] UpdateAudiobookDto audiobookDto)
        {
            var response = await _audioBookService.UpdateAudiobookAsync(audiobookDto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAudiobook(int audiobookId)
        {
            var response = await _audioBookService.DeleteAudiobook(audiobookId);
            if (!response.Success)
                return NotFound(response); 

            return Ok(response); 
        }



    }
}
