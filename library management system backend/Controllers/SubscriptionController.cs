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
        [HttpGet("durations")]
        public async Task<IActionResult> GetDurations()
        {
            var durations = await _subscriptionService.GetDurationsAsync();
            if (durations == null || durations.Count == 0)
            {
                return NotFound("No payment durations found.");
            }
            return Ok(durations);
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
        public async Task<IActionResult> Subscribe([FromQuery] SubscribeRequest model)
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
        [HttpGet("history")]
        public async Task<IActionResult> GetSubscriptionHistory([FromQuery] int? userId)
        {
            var subscriptionHistory = await _subscriptionService.GetSubscriptionHistory(userId);

            if (subscriptionHistory == null || subscriptionHistory.Count == 0)
            {
                return NotFound("No subscription history found.");
            }

            return Ok(subscriptionHistory);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveSubscriptionsWithDetails([FromQuery] int? userId)
        {
            var activeSubscriptions = await _subscriptionService.GetActiveSubscriptionsWithDetailsAsync(userId);

            if (activeSubscriptions == null || !activeSubscriptions.Any())
            {
                return NotFound("No active subscriptions found.");
            }

            return Ok(activeSubscriptions);
        }
    }
}
