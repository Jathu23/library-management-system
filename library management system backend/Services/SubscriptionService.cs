using Azure.Core;
using library_management_system.Database.Entiy;
using library_management_system.DTOs;
using library_management_system.DTOs.Subscription;
using library_management_system.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Services
{
    public class SubscriptionService
    {
        private readonly SubscriptionRepository _subscriptionRepository;
        private readonly UserRepo _userRepo;

        public SubscriptionService(SubscriptionRepository subscriptionRepository, UserRepo userRepo)
        {
            _subscriptionRepository = subscriptionRepository;
            _userRepo = userRepo;
        }

        public async Task<List<SubscriptionPlan>> GetAllSubscriptionPlansAsync()
        {
            return await _subscriptionRepository.GetAllSubscriptionPlansAsync();
        }

        public async Task<SubscriptionPlan?> GetSubscriptionPlanByIdAsync(int id)
        {
            return await _subscriptionRepository.GetSubscriptionPlanByIdAsync(id);
        }
        public async Task<List<PaymentDuration>> GetDurationsAsync()
        {
            return await _subscriptionRepository.GetDurationsAsync();
        }

        public async Task<ApiResponse<Payment>> SubscribeAsync(SubscribeRequest request)
        {   
            var response = new ApiResponse<Payment>();
            var user = await _userRepo.Getuserid(request.UserId);
            if (user.IsSubscribed)
            {
                throw new Exception("Alredy subscription Add");
            }
            // Fetch subscription plan details
            var subscriptionPlan = await _subscriptionRepository.GetSubscriptionPlanByIdAsync(request.SubscriptionPlanId);
            if (subscriptionPlan == null)
            {
                throw new Exception("Invalid subscription plan.");
               
            }

            // Fetch payment duration details
            var paymentDuration = await _subscriptionRepository.GetPaymentDurationByIdAsync(request.PaymentDurationId);
            if (paymentDuration == null)
            {
                throw new Exception("Invalid payment duration.");
                  
            }

            // Calculate payment amount (e.g., amount = price * multiplier)
            var amountToPay = subscriptionPlan.Price * paymentDuration.Multiplier;
            var payment = new Payment
            {
                UserId = request.UserId,
                SubscriptionPlanId =request.SubscriptionPlanId,
                PaymentDurationId = request.PaymentDurationId,
                AmountPaid = amountToPay,
                PaymentDate = DateTime.Now,
                PaymentMethod = request.PayMethod,
                Status = "Successful", // Assuming the payment is successful
                TransactionId = Guid.NewGuid().ToString(), // Generate a unique transaction ID
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            // Process payment (assume external payment processing)
            var paymentResult = await _subscriptionRepository.AddPaymentAsync(payment);

            if (paymentResult == null)
            {
                throw new Exception("error while payment processing.");
            }

            // If payment is successful, create a user subscription
            var userSubscription = new UserSubscription
            {
                UserId = request.UserId,
                SubscriptionPlanId = request.SubscriptionPlanId,
                PaymentDurationId= request.PaymentDurationId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(paymentDuration.Duration), // Calculate end date based on the duration
                Status = "Active",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

                var data = await _subscriptionRepository.AddUserSubscriptionAsync(userSubscription);

            if (data == null)
            {
                throw new Exception("error while  processing.");
            }

            user.IsSubscribed = true;
            await _userRepo.UpdateUser(user);
            // Return success response
            response.Success = true;
            response.Message = "successful, create a subscription";
            response.Data = paymentResult;
            return response;
        }

        public async Task<bool> CancelSubscriptionAsync(int userId)
        {
            var user = await _userRepo.Getuserid(userId);

            // Get the active subscription
            var activeSubscription = await _subscriptionRepository.GetUserSubscriptionByUserIdAsync( userId);
            if (activeSubscription == null)
                throw new Exception("No active subscription found for this user.");

            // Update subscription status to 'Cancelled'
            user.IsSubscribed=false;
            activeSubscription.Status = "Cancelled";
            activeSubscription.UpdatedDate = DateTime.Now;
            await _userRepo.UpdateUser(user);

            return await _subscriptionRepository.UpdateUserSubscriptionAsync(activeSubscription);
        }
    }
}
