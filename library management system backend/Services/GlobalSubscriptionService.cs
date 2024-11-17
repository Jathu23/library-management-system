using library_management_system.Database.Entiy;
using library_management_system.DTOs;
using library_management_system.DTOs.Subscription;
using library_management_system.Repositories;

namespace library_management_system.Services
{
    public class GlobalSubscriptionService 
    {
        private readonly GlobalSubscriptionRepository _repository;

        public GlobalSubscriptionService(GlobalSubscriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<string>> CreateOrRenewSubscriptionAsync(GlobalSubscriptionDto subscriptionDto)
        {
            var existingSubscription = await _repository.GetByUserIdAsync(subscriptionDto.UserId);
            var user = await _repository.GetUserById(subscriptionDto.UserId);

            if (user == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "User not found.",
                    Data = null
                };
            }

            // Validate the payment amount based on the subscription type
            if (subscriptionDto.SubscriptionType == "Monthly" && subscriptionDto.Amount != 3)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid payment amount for Monthly subscription.",
                    Data = null
                };
            }

            if (subscriptionDto.SubscriptionType == "Yearly" && subscriptionDto.Amount != 20)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid payment amount for Yearly subscription.",
                    Data = null
                };
            }

            if (existingSubscription != null && existingSubscription.EndDate >= DateTime.UtcNow)
            {
                // Renew the existing subscription
                existingSubscription.EndDate = subscriptionDto.SubscriptionType == "Monthly"
                    ? existingSubscription.EndDate.AddMonths(1)
                    : existingSubscription.EndDate.AddYears(1);

                user.IsSubscribed = true;
                await _repository.UpdateUserAsync(user);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "Subscription renewed successfully.",
                    Data = $"New End Date: {existingSubscription.EndDate}"
                };
            }
            else
            {
                // Create a new subscription
                var newSubscription = new GlobalSubscription
                {
                    UserId = subscriptionDto.UserId,
                    SubscriptionType = subscriptionDto.SubscriptionType,
                    StartDate = DateTime.UtcNow,
                    Amount = subscriptionDto.Amount,
                    EndDate = subscriptionDto.SubscriptionType == "Monthly"
                        ? DateTime.UtcNow.AddMonths(1)
                        : DateTime.UtcNow.AddYears(1),
                    IsActive = true,
                };

                await _repository.AddSubscriptionAsync(newSubscription);

                user.IsSubscribed = true;
                await _repository.UpdateUserAsync(user);

                return new ApiResponse<string>
                {
                    Success = true,
                    Message = "New subscription created successfully.",
                    Data = $"Subscription End Date: {newSubscription.EndDate}"
                };
            }
        }





        public async Task<object> CheckSubscriptionStatusAsync(int userId)
        {
            var subscription = await _repository.GetByUserIdAsync(userId);

            if (subscription == null || subscription.EndDate < DateTime.UtcNow)
                return new { isActive = false, message = "No active subscription." };

            return new
            {
                isActive = true,
                amount = subscription.Amount,
                subscriptionType = subscription.SubscriptionType,
                startDate = subscription.StartDate,
                endDate = subscription.EndDate
            };
        }




    }

}
