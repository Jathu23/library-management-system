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
        private readonly JwtService _jwtService;

        public UserController(UserServices userServices, JwtService jwtService)
        {
            _userService = userServices;
            _jwtService = jwtService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequstModel userRequestDto)
        {
            var response = new ApiResponse<string>();

            if (!ModelState.IsValid)
            {
                response.Success = false;
                response.Message = "Validation failed.";
                response.Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(response);
            }

            try
            {
              
                var createdUser = await  _userService.CreateUser(userRequestDto);

                if (createdUser == null)
                {
                    response.Success = false;
                    response.Message = "Failed to create user.";
                    return BadRequest(response);
                }

              
                var token = _jwtService.GenerateToken(createdUser);

           
                response.Success = true;
                response.Message = "User created successfully.";
                response.Data = token;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the user.";
                response.Errors.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }








    }
}
