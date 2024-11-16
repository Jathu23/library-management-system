using library_management_system.Database.Entiy;
using library_management_system.DTOs.Subscription;
using library_management_system.DTOs;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GlobalSubscriptionController : ControllerBase
    {
        private readonly GlobalSubscriptionService _service;

        public GlobalSubscriptionController(GlobalSubscriptionService service)
        {
            _service = service;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] GlobalSubscriptionDto subscriptionDto)
        {
            if (subscriptionDto == null || string.IsNullOrEmpty(subscriptionDto.SubscriptionType))
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid subscription data.",
                    Errors = new List<string> { "Subscription type is required." }
                });

            var subscription = await _service.CreateOrRenewSubscriptionAsync(subscriptionDto);
            var response = new ApiResponse<GlobalSubscriptionDto>
            {
                Success = true,
                Message = "Subscription successful.",
                Data = subscriptionDto
            };

            return Ok(response);
        }


        // Check Subscription Status
        [HttpGet("status/{userId}")]
        public async Task<IActionResult> CheckSubscriptionStatus(int userId)
        {
            var status = await _service.CheckSubscriptionStatusAsync(userId);

            if (status == null)
                return NotFound("No active subscription found.");

            return Ok(status);
        }
    }

}
