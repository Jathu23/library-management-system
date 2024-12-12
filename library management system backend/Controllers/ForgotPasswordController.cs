using library_management_system.DTOs;
using library_management_system.DTOs.ForgotPassword;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ForgotPasswordController : ControllerBase
{
    private readonly ForgotPasswordService _forgotPasswordService;

    public ForgotPasswordController(ForgotPasswordService forgotPasswordService)
    {
        _forgotPasswordService = forgotPasswordService;
    }

    // Endpoint to generate and send OTP
    [HttpPost("send-token")]
    public async Task<IActionResult> SendToken([FromQuery] ForgotPasswordRequests request)
    {
        if (string.IsNullOrEmpty(request.Email))
            return BadRequest(new ApiResponse<string> { Success = false, Message = "Email is required." });

        var response = await _forgotPasswordService.GenerateAndSendTokenAsync(request.Email);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    // Endpoint to validate OTP and update password
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordRequests request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.OtpCode) || string.IsNullOrEmpty(request.NewPassword))
            return BadRequest(new ApiResponse<string> { Success = false, Message = "Email, OTP, and new password are required." });

        var response = await _forgotPasswordService.ValidateTokenAndUpdatePasswordAsync(request.Email, request.OtpCode, request.NewPassword);

        return response.Success ? Ok(response) : BadRequest(response);
    }
}
