using library_management_system.Database.Entiy;
using library_management_system.DTOs.Ebook;
using library_management_system.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static library_management_system.DTOs.Ebook.UpdateEbookDto;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EbookController : ControllerBase
    {
        private readonly EbookService _ebookService;

        public EbookController(EbookService ebookService)
        {
            _ebookService = ebookService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEbook( AddEbookDto ebookDto)
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

        [HttpGet("GetEbooks")]
        public async Task<IActionResult> GetPaginatedEbooks(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _ebookService.GetEbooksWithPagination(pageNumber, pageSize);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchEbooks(
        [FromQuery] string searchString,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            var response = await _ebookService.SearchEbooksAsync(searchString, pageNumber, pageSize);
            return Ok(response);
        }

        [HttpPost("AddClick")]
        public async Task<IActionResult> AddClick(int bookid)
        {
            var result = await _ebookService.AddClick(bookid);
            if (result)
                return Ok(result);
            else
                return BadRequest(result);

        }

        [HttpGet("top")]
        public async Task<ActionResult<List<Ebook>>> GetTopEbooksAsync(int count)
        {
            try
            {
                var ebooks = await _ebookService.GetTopEbooksAsync(count);

                // Check if no eBooks found
                if (ebooks == null || ebooks.Count == 0)
                {
                    return NotFound("No eBooks found.");
                }

                return Ok(ebooks); // Return the eBooks with a 200 OK status
            }
            catch (Exception ex)
            {


                // Return a 500 Internal Server Error with a message
                return StatusCode(500, "An error occurred while fetching top eBooks. Please try again later.");
            }


        }

        [HttpGet("ebooks/count")]
        public async Task<IActionResult> GetEbookCount()
        {
            int count = await _ebookService.GetEbookCountAsync();
            return Ok(new { Count = count });
        }


        //        [HttpPost ("sample")]
        //        public async Task<int> addsample()
        //        {
        //            var ebooks = new List<AddEbookDto>
        //{
        //    new AddEbookDto
        //    {
        //        ISBN = "978-1-234567-01-1",
        //        Title = "The Digital Frontier",
        //        Author = "Sophia Adams",
        //        Genre = "Technology",
        //        PublishYear = 2021,
        //        EbookFile = null,
        //        CoverImages = null,
        //        Metadata = new EbookMetadataDto
        //        {
        //            Language = "English",
        //            Publisher = "TechPioneers",
        //            Description = "Exploring the cutting edge of technology.",
        //            DigitalRights = "DRM Protected"
        //        }
        //    },
        //    new AddEbookDto
        //    {
        //        ISBN = "978-1-234567-02-1",
        //        Title = "Mysteries of the Mind",
        //        Author = "Ethan Johnson",
        //        Genre = "Psychology",
        //        PublishYear = 2019,
        //        EbookFile = null,
        //        CoverImages = null,
        //        Metadata = new EbookMetadataDto
        //        {
        //            Language = "English",
        //            Publisher = "Mind Matters",
        //            Description = "A deep dive into the human mind.",
        //            DigitalRights = "Open Access"
        //        }
        //    },
        //    new AddEbookDto
        //    {
        //        ISBN = "978-1-234567-03-1",
        //        Title = "Chronicles of the Cosmos",
        //        Author = "Liam Brown",
        //        Genre = "Science Fiction",
        //        PublishYear = 2020,
        //        EbookFile = null,
        //        CoverImages = null,
        //        Metadata = new EbookMetadataDto
        //        {
        //            Language = "English",
        //            Publisher = "Galaxy Tales",
        //            Description = "An interstellar journey through time and space.",
        //            DigitalRights = "DRM Protected"
        //        }
        //    },
        //    new AddEbookDto
        //    {
        //        ISBN = "978-1-234567-04-1",
        //        Title = "The Art of Resilience",
        //        Author = "Olivia Taylor",
        //        Genre = "Self-Help",
        //        PublishYear = 2022,
        //        EbookFile = null,
        //        CoverImages = null,
        //        Metadata = new EbookMetadataDto
        //        {
        //            Language = "English",
        //            Publisher = "Wellness Press",
        //            Description = "Empowering strategies for personal growth.",
        //            DigitalRights = "Open Access"
        //        }
        //    },
        //    new AddEbookDto
        //    {
        //        ISBN = "978-1-234567-05-1",
        //        Title = "Legends of the Wild",
        //        Author = "Charlotte Lee",
        //        Genre = "Adventure",
        //        PublishYear = 2018,
        //        EbookFile = null,
        //        CoverImages = null,
        //        Metadata = new EbookMetadataDto
        //        {
        //            Language = "English",
        //            Publisher = "Nature Chronicles",
        //            Description = "A tale of survival and courage.",
        //            DigitalRights = "Public Domain"
        //        }
        //    },

        //};





        //            for (int i = 6; i <= 30; i++)
        //            {
        //                ebooks.Add(new AddEbookDto
        //                {
        //                    ISBN = $"978-1-234567-{i:02}-1",
        //                    Title = $"Ebook Title {i}",
        //                    Author = $"Author {i}",
        //                    Genre = (i % 2 == 0) ? "Fiction" : "Non-Fiction",
        //                    PublishYear = 2015 + i % 10,
        //                    EbookFile = null,
        //                    CoverImages = null,
        //                    Metadata = new EbookMetadataDto
        //                    {
        //                        Language = "English",
        //                        Publisher = $"Publisher {i}",
        //                        Description = $"A description for Ebook Title {i}.",
        //                        DigitalRights = (i % 2 == 0) ? "DRM Protected" : "Open Access"
        //                    }
        //                });
        //            }

        //        }
    }
}
