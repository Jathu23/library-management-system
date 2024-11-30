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

        [HttpGet("byid")]
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

        [HttpDelete("byid")]
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
        [HttpGet("UserAccountActive")]
        public async Task<IActionResult> ActiveUserAccount([FromQuery] int id)
        {
            var response = await _adminServices.ActiveteUserAccount(id);
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

        [HttpPost("addsampleadmin")]
        public async Task<IActionResult> AddSampleAdmin([FromQuery] int howmany = 5)
        {
            try
            {
                for (int i = 0; i < howmany; i++)
                {
                    
                    var newAdmin = new AdminRequstModel()
                    {
                        AdminNic = $"00{i + 1}v", 
                        FirstName = "Admin", 
                        LastName = $"00{i + 1}",
                        Email = $"admin0{i + 1}@example.com",
                        Password = "2", 
                        ProfileImage = null 
                    };

                        await _adminServices.CreateAdmin(newAdmin);
                    Thread.Sleep(500);
                   
                }

                return Ok($"Successfully created {howmany} admins.");
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
