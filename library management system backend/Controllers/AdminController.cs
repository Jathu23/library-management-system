using library_management_system.DTOs.User;
using library_management_system.DTOs;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using library_management_system.DTOs.Admin;
using System.Diagnostics.Contracts;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminServices _adminServices;
        private readonly Email _Email;


        public AdminController(AdminServices adminServices, Email email)
        {
            _adminServices = adminServices;
            _Email = email;
        }


        [HttpPost("CreateAdmin")]
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


            const string subject = "Account Admin Account is Created";

            var body = $"""
                <html>
                    <body>
                        <h1>Hello, {AdminRequstDto.FirstName} {AdminRequstDto.LastName}</h1>
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

            await _Email.SendEmailAsync(AdminRequstDto.Email, subject, body);

            return Ok(response);

        }


        //[HttpPost("Aminlogin")]
        //public async Task<IActionResult> LoginUser([FromBody] AdminLoginRequset adminLoginRequset)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var response1 = new ApiResponse<string>
        //        {
        //            Success = false,
        //            Message = "Validation failed.",
        //            Errors = ModelState.Values
        //                .SelectMany(v => v.Errors)
        //                .Select(e => e.ErrorMessage)
        //                .ToList()
        //        };
        //        return BadRequest(response1);
        //    }

        //    var response = await _adminServices.LoginAdmin(adminLoginRequset);

        //    if (!response.Success)
        //        return BadRequest(response);

        //    return Ok(response);
        //}


        [HttpGet("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var response = await _adminServices.GetAllAdmins();

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById(int id)
        {
            var response = await _adminServices.GetAdminById(id);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminById(int id)
        {
            var response = await _adminServices.DeleteAdminById(id);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpPut("UseAccountActive")]
        public async Task<IActionResult> ActiveUserAccount(string nicOrEmail)
        {
            var response = await _adminServices.ActiveteUserAccount(nicOrEmail);
            if (response != null)
            {
                return Ok(response);
            }
            else

            {
                return BadRequest(response);
            } }

        [HttpPost("transfer-master-control")]
        public async Task<IActionResult> TransferMasterControl([FromQuery] int CurrentMasterId, [FromQuery] int NewMasterId)
        {
            var response = await _adminServices.TransferMasterControlAsync(CurrentMasterId, NewMasterId);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
