using Azure.Core;
using library_management_system.Database.Entiy;
using library_management_system.DTOs;
using library_management_system.DTOs.Book;
using library_management_system.DTOs.User;
using library_management_system.Services;
using library_management_system.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersOne.Epub.Schema;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userService;
        private readonly Email _Email;



        public UserController(UserServices userServices, AudioBookFileService emailServices, Email Email)
        {
            _userService = userServices;
            _Email = Email;


        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(UserRequstModel userRequestDto)
        {
            if (!ModelState.IsValid)
            {
                var response1 = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Validation failed.",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                };
                return BadRequest(response1);
            }
            var response = await _userService.CreateUser(userRequestDto);

            if (!response.Success)
                return BadRequest(response);


            const string subject = "Account Created";

            var body = $"""
                <html>
                    <body>
                        <h1>Hello, {userRequestDto.FirstName} {userRequestDto.LastName}</h1>
                        <h2>
                            Your account has been created and we have sent approval request to admin.
                            Once the request is approved by admin you will receive email, and you will be
                            able to login in to your account.
                        </h2>
                        <h3>Thanks</h3>
                    </body>
                </html>
            
            """
            ;

            await _Email.SendEmailAsync(userRequestDto.Email, subject, body);

            return Ok(response);




        }
        [HttpGet("GetAllActiveUsers")]
        public async Task<IActionResult> GetAllActiveUsersWithPagination([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {

            var response = await _userService.GetAllActiveUsers(pageNumber, pageSize);


            if (!response.Success)
            {
                return BadRequest(new
                {
                    response.Message
                });
            }

            return Ok(new
            {
                response.Message,
                response.Data
            });
        }
        [HttpDelete("soft-delete-user")]
        public async Task<IActionResult> SoftDeleteUser(int id)
        {
            var response = await _userService.SoftDelete(id);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpGet("getSingleUserByNICorEmail")]
        public async Task<IActionResult> GetUser(string emailorNic)
        {
            var data = await _userService.GetUserByNICorEmail(emailorNic);
            if (data == null)
            {
                return BadRequest(Response);
            }
            else
            {
                return Ok(data);
            }

        }
        [HttpGet("GetAllDisabledUsers")]
        public async Task<IActionResult> GetAllNonactiveUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {

            var response = await _userService.GetAllNonactiveUsers(pageNumber, pageSize);


            if (!response.Success)
            {
                return BadRequest(new
                {
                    response.Message
                });
            }

            return Ok(new
            {
                response.Message,
                response.Data
            });
        }

        [HttpDelete("DeletePermanantly")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUserPermanently(id);

            if (!response.Success)
                return NotFound(new { message = response.Message });

            return Ok(new { message = response.Message, user = response.Data });
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserInfoUpdateDto userInfoUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Validation failed.",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            var response = await _userService.UpdateUser(userInfoUpdateDto);

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
            var response = await _userService.SearchUsersAsync(searchString, pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("GetUserEmails")]
        public async Task<IActionResult> GetUserEmailsByPrefix([FromQuery] string prefix)
        {
            var response = await _userService.GetUserEmailsByPrefix(prefix);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpGet("subscribed-users")]
        public async Task<IActionResult> GetSubscribedUsersWithPagination([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {

            var response = await _userService.GetSubscribedUsersWithPagination(pageNumber, pageSize);


            if (!response.Success)
            {
                return BadRequest(new
                {
                    response.Message
                });
            }

            return Ok(new
            {
                response.Message,
                response.Data
            });
        }
        [HttpGet("report")]
        public async Task<IActionResult> GetOverallUserReport()
        {
            try
            {
                var report = await _userService.GetOverallUserReport();
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPost("sampleuseradd")]
        public async Task<IActionResult> AddSampleUser([FromQuery] int howmany = 2)
        {
            try
            {
                for (int i = 0; i < howmany; i++)
                {
                    
                    var newUser = new UserRequstModel()
                    {
                        UserNic = $"0{i + 1}", 
                        FirstName = "User", 
                        LastName = $"00{i + 1}",
                        Email = $"user0{i + 1}@example.com",
                        PhoneNumber = $"076814520{i}", 
                        Address = $"Address00 {i + 1}",
                        Password = "1", 
                        ProfileImage = null
                    };

                  
                    await _userService.CreateUser(newUser);
                    Thread.Sleep(500);
                   
                }

                return Ok($"Successfully created {howmany} users.");
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



		//Functions for dashboard

		[HttpGet("count-all-users")]
		public async Task<IActionResult> GetUserCountAsync()
		{
			try
			{
				int userCount = await _userService.GetAllUsersCountAsync();
				return Ok(userCount);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("active-count")]
		public async Task<IActionResult> GetActiveUserCountAsync()
		{
			try
			{
				int activeUserCount = await _userService.GetActiveUserCountAsync();
				return Ok(activeUserCount);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("non-active-count")]
		public async Task<IActionResult> GetNonActiveUserCountAsync()
		{
			try
			{
				int activeUserCount = await _userService.GetNonActiveUserCountAsync();
				return Ok(activeUserCount);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("subscribed-user-count")]
		public async Task<IActionResult> GetSubscribedUserCountAsync()
		{
			try
			{
				int activeUserCount = await _userService.GetSubscribedUserCountAsync();
				return Ok(activeUserCount);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

       [ HttpGet("getUserByIdid")]
 public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserById(id);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }


    }
}
