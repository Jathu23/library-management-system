using library_management_system.DTOs.AudioBook;
using library_management_system.Repositories;
using library_management_system.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AudiobookController : ControllerBase
    {
        private readonly AudioBookService _audioBookService;

        public AudiobookController(AudioBookService audioBookService)
        {
            _audioBookService = audioBookService;
        }

        [HttpPost("add-audiobook")]
        public async Task<IActionResult> AddAudiobook( AddAudiobookDto audiobookDto)
        {
            var response = await _audioBookService.AddAudiobook(audiobookDto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("update-audiobook")]
        public async Task<IActionResult> UpdateAudiobook( UpdateAudiobookDto audiobookDto)
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

        [HttpGet("GetAudiobooks")]
        public async Task<IActionResult> GetAudiobooks(int page = 1, int pageSize = 10)
        {
            var response = await _audioBookService.GetAudiobooksAsync(page, pageSize);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(
       [FromQuery] string searchString,
       [FromQuery] int pageNumber = 1,
       [FromQuery] int pageSize = 10)
        {
            var response = await _audioBookService.SearchAudioBooksAsync(searchString, pageNumber, pageSize);
            return Ok(response);
        }


		//displaying some Audion books--------------------------

		[HttpGet("top/{count:int}")]
		public async Task<IActionResult> GetTopAudiobooks(int count)
		{
			if (count <= 0)
			{
				return BadRequest("Count must be a positive integer.");
			}

			var topAudiobooks = await _audioBookService.GetTopAudiobooksAsync(count);

			if (topAudiobooks == null || !topAudiobooks.Any())
			{
				return NotFound("No audiobooks found.");
			}

			return Ok(topAudiobooks);
		}

        [HttpPost ("AddClick")]
        public async Task<IActionResult> AddClick(int bookid)
        {
            var result = await _audioBookService.AddClick(bookid);
            if (result)
                return Ok(result);
            else
                return BadRequest(result);

        }


        //        [HttpPost("addsamle")]
        //        public async Task<int> addsample()
        //        {
        //            var audiobooks = new List<AddAudiobookDto>
        //{
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-01-0",
        //        Title = "The Silent World",
        //        Author = "Jane Doe",
        //        Genre = "Science Fiction",
        //        PublishYear = 2021,
        //        AudioFile = null,  // Replace with actual IFormFile during runtime
        //        CoverImage = null, // Replace with actual IFormFile during runtime
        //        FileFormat = "MP3",
        //        Language = "English",
        //        Narrator = "John Smith",
        //        Publisher = "Future Press",
        //        Description = "A thrilling journey into a silent world.",
        //        DigitalRights = "DRM Protected"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-02-0",
        //        Title = "Whispers in the Dark",
        //        Author = "Emily Carter",
        //        Genre = "Mystery",
        //        PublishYear = 2020,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "MP3",
        //        Language = "English",
        //        Narrator = "Samantha Lee",
        //        Publisher = "Mystery House",
        //        Description = "A suspenseful tale of whispers and shadows.",
        //        DigitalRights = "Open Access"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-03-0",
        //        Title = "The Art of Mindfulness",
        //        Author = "Robert Kim",
        //        Genre = "Self-Help",
        //        PublishYear = 2019,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "AAC",
        //        Language = "English",
        //        Narrator = "Karen Moore",
        //        Publisher = "Mindful Reads",
        //        Description = "Practical techniques to improve mindfulness.",
        //        DigitalRights = "DRM Protected"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-04-0",
        //        Title = "The Cosmic Voyage",
        //        Author = "Chris Nolan",
        //        Genre = "Science",
        //        PublishYear = 2018,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "WAV",
        //        Language = "English",
        //        Narrator = "James Taylor",
        //        Publisher = "Astro Books",
        //        Description = "Explore the cosmos with this engaging audiobook.",
        //        DigitalRights = "Open Access"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-05-0",
        //        Title = "Adventures in Wonderland",
        //        Author = "Lewis Carroll",
        //        Genre = "Fantasy",
        //        PublishYear = 2017,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "MP3",
        //        Language = "English",
        //        Narrator = "Mary Collins",
        //        Publisher = "Classic Tales",
        //        Description = "A classic tale of curiosity and adventure.",
        //        DigitalRights = "Public Domain"
        //    },
        //     new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-06-0",
        //        Title = "Echoes of Eternity",
        //        Author = "Rachel Adams",
        //        Genre = "Historical Fiction",
        //        PublishYear = 2022,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "MP3",
        //        Language = "English",
        //        Narrator = "Evelyn Harper",
        //        Publisher = "Timeless Tales",
        //        Description = "A journey through time and love.",
        //        DigitalRights = "DRM Protected"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-07-0",
        //        Title = "The Quantum Detective",
        //        Author = "Harrison Ford",
        //        Genre = "Science Fiction",
        //        PublishYear = 2020,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "AAC",
        //        Language = "English",
        //        Narrator = "Liam Watson",
        //        Publisher = "Futuristic Reads",
        //        Description = "A detective story set in a quantum universe.",
        //        DigitalRights = "Open Access"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-08-0",
        //        Title = "The Mindful Leader",
        //        Author = "Sophia Bennett",
        //        Genre = "Business",
        //        PublishYear = 2019,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "WAV",
        //        Language = "English",
        //        Narrator = "Michael Davis",
        //        Publisher = "Leadership Insights",
        //        Description = "Transform your leadership with mindfulness techniques.",
        //        DigitalRights = "DRM Protected"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-09-0",
        //        Title = "Lost in the Stars",
        //        Author = "Olivia Carter",
        //        Genre = "Adventure",
        //        PublishYear = 2018,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "MP3",
        //        Language = "English",
        //        Narrator = "Emma Wilson",
        //        Publisher = "Starry Nights",
        //        Description = "A space adventure like no other.",
        //        DigitalRights = "Open Access"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-10-0",
        //        Title = "Tales of the Ancient Woods",
        //        Author = "Liam Brown",
        //        Genre = "Fantasy",
        //        PublishYear = 2017,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "MP3",
        //        Language = "English",
        //        Narrator = "Sophia Green",
        //        Publisher = "Nature Chronicles",
        //        Description = "Legends and myths from the ancient forests.",
        //        DigitalRights = "Public Domain"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-11-0",
        //        Title = "The Art of Negotiation",
        //        Author = "John Maxwell",
        //        Genre = "Self-Help",
        //        PublishYear = 2021,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "MP3",
        //        Language = "English",
        //        Narrator = "David Ross",
        //        Publisher = "Success Stories",
        //        Description = "Master negotiation skills to excel in any field.",
        //        DigitalRights = "DRM Protected"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-12-0",
        //        Title = "Beyond the Horizon",
        //        Author = "Charlotte Lewis",
        //        Genre = "Adventure",
        //        PublishYear = 2020,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "WAV",
        //        Language = "English",
        //        Narrator = "Daniel Martinez",
        //        Publisher = "Wanderlust Books",
        //        Description = "An epic tale of exploration and discovery.",
        //        DigitalRights = "Open Access"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-13-0",
        //        Title = "The Hidden Truth",
        //        Author = "Emma Thompson",
        //        Genre = "Thriller",
        //        PublishYear = 2019,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "MP3",
        //        Language = "English",
        //        Narrator = "James Evans",
        //        Publisher = "Mystery House",
        //        Description = "Unravel the secrets hidden within.",
        //        DigitalRights = "DRM Protected"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-14-0",
        //        Title = "Legends of the Unknown",
        //        Author = "Thomas Walker",
        //        Genre = "Fantasy",
        //        PublishYear = 2018,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "AAC",
        //        Language = "English",
        //        Narrator = "Sophia Miller",
        //        Publisher = "Fictional World",
        //        Description = "Ancient legends brought to life in an enchanting tale.",
        //        DigitalRights = "Public Domain"
        //    },
        //    new AddAudiobookDto
        //    {
        //        ISBN = "978-1-234567-15-0",
        //        Title = "Winds of Change",
        //        Author = "Isabella Clarke",
        //        Genre = "Drama",
        //        PublishYear = 2022,
        //        AudioFile = null,
        //        CoverImage = null,
        //        FileFormat = "MP3",
        //        Language = "English",
        //        Narrator = "Olivia Martinez",
        //        Publisher = "Drama Hub",
        //        Description = "A gripping tale of love and resilience.",
        //        DigitalRights = "Open Access"
        //    }
        //};

        //        }



    }
}
