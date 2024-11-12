using Azure.Core;
using library_management_system.Database.Entiy;
using library_management_system.DTOs;
using library_management_system.DTOs.User;
using library_management_system.Services;
using library_management_system.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userService;
       

        public UserController(UserServices userServices)
        {
            _userService = userServices;
          
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

        return Ok(response);

        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest loginRequest)
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

            var response = await _userService.LoginUser(loginRequest);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }







    }
}
