using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class SubscriptionRepository
    {
        private readonly LibraryDbContext _context;

        public SubscriptionRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<SubscriptionPlan?> GetSubscriptionPlanByIdAsync(int id)
        {
            return await _context.SubscriptionPlan
                .FindAsync(id);
        }

        public async Task<List<SubscriptionPlan>> GetAllSubscriptionPlansAsync()
        {
            return await _context.SubscriptionPlan.ToListAsync();
        }

        public async Task<UserSubscription?> GetUserSubscriptionByUserIdAsync(int userId)
        {
            return await _context.UserSubscription
         .Include(us => us.SubscriptionPlan)
         .Include(us => us.PaymentDuration)
         .FirstOrDefaultAsync(us => us.UserId == userId && us.Status == "Active" && us.EndDate >= DateTime.UtcNow);
        }
      
        public async Task<bool> UpdateUserSubscriptionAsync(UserSubscription subscription)
        {
            _context.UserSubscription.Update(subscription);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserSubscription> AddUserSubscriptionAsync(UserSubscription userSubscription)
        {
            await _context.UserSubscription.AddAsync(userSubscription);
            await _context.SaveChangesAsync();
            return userSubscription;
        }

        public async Task<PaymentDuration?> GetPaymentDurationByIdAsync(int id)
        {
            return await _context.PaymentDuration
                .FirstOrDefaultAsync(pd => pd.Id == id);
        }

        public async Task<List<PaymentDuration>> GetDurationsAsync()
        {
            return await _context.PaymentDuration.ToListAsync();
        }
        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            await _context.Payment.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<SubscriptionPlan?> GetFreePlanAsync()
        {
            return await _context.SubscriptionPlan
                .FirstOrDefaultAsync(sp => sp.Name == "Free");
        }
        public async Task<List<UserSubscription>> GetSubscriptionHistory(int? userId = null)
        {
            var query = _context.UserSubscription
                .Include(us => us.SubscriptionPlan)
                .Include(us => us.PaymentDuration)
                .Include(us => us.User)
                //.Where(us => us.Status != "Active" )
                .AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(us => us.UserId == userId.Value);
            }

            return await query
                .OrderByDescending(us => us.StartDate) // Sort by most recent
                .ToListAsync();
        }
        public async Task<List<UserSubscription>> GetActiveSubscriptionsWithDurationAsync(int? userId = null)
        {
            var query = _context.UserSubscription
                .Include(us => us.SubscriptionPlan)
                .Include(us => us.PaymentDuration)
                .Where(us => us.Status == "Active" && us.EndDate > DateTime.Now)
                .AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(us => us.UserId == userId.Value);
            }

            return await query.ToListAsync();
        }

    }
}
