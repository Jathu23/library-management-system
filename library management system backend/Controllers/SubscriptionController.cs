using library_management_system.DTOs.Subscription;
using library_management_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly SubscriptionService _subscriptionService;

        public SubscriptionController(SubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("plans")]
        public async Task<IActionResult> GetAllSubscriptionPlans()
        {
            var plans = await _subscriptionService.GetAllSubscriptionPlansAsync();
            return Ok(plans);
        }

        //[HttpGet("plans")]
        //public async Task<IActionResult> GetSubscriptionPlanById(int id)
        //{
        //    var plan = await _subscriptionService.GetSubscriptionPlanByIdAsync(id);
        //    if (plan == null)
        //    {
        //        return NotFound("Subscription plan not found.");
        //    }
        //    return Ok(plan);
        //}



        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscribeRequest model)
        {
            // Call service to handle the subscription process
            var result = await _subscriptionService.SubscribeAsync(model);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelSubscription([FromQuery] int userId)
        {
          
                var result = await _subscriptionService.CancelSubscriptionAsync(userId);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }
    }
}
