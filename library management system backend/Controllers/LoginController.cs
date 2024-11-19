using library_management_system.DTOs.LoginPort;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginRequstDto loginRequest)
        {
            

            var response = await _loginService.Login(loginRequest);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
