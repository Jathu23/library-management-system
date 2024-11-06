using Azure.Core;
using library_management_system.Database.Entiy;
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
        private readonly UserServices _userServices;
        private readonly JwtService _jwtService;

        public UserController(UserServices userServices, JwtService jwtService)
        {
            _userServices = userServices;
            _jwtService = jwtService;
        }

        [HttpPost ("Add")]
        public User Add(UserRequstModel user)
        {
            return _userServices.Add(user);
        }

        [HttpGet ("login")]
        public IActionResult get(string nic,string pass)
        {
            if (nic == "1" && pass == "1")
            {
                var token = _jwtService.GenerateToken(user);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }


        User user = new User
        {
            NIC = "123456789V",
            Name = "John Doe",
            Email = "johndoe@example.com",
            PasswordHash = "hashed_password_here" 
        };

    }
}
