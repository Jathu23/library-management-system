using library_management_system.DTOs.User;
using library_management_system.DTOs;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using library_management_system.DTOs.Admin;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminServices _adminServices;

        public AdminController(AdminServices adminServices)
        {
            _adminServices = adminServices;
        }


        [HttpPost("Admincreate")]
        public async Task<IActionResult> CreateAdmin(AdminRequstModel AdminRequstDto)
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
            var response = await _adminServices.CreateAdmin(AdminRequstDto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);

        }


        [HttpPost("Aminlogin")]
        public async Task<IActionResult> LoginUser([FromBody] AdminLoginRequset adminLoginRequset)
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

            var response = await _adminServices.LoginAdmin(adminLoginRequset);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
