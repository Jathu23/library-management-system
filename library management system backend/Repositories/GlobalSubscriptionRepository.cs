using library_management_system.Database.Entiy;
using library_management_system.Database;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class GlobalSubscriptionRepository 
    {
        private readonly LibraryDbContext _context;

        public GlobalSubscriptionRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddSubscriptionAsync(GlobalSubscription subscription)
        {
             _context.globalSubscriptions.AddAsync(subscription);
            await  _context.SaveChangesAsync();
        }


        public async Task<GlobalSubscription> GetByUserIdAsync(int userId)
        {
            return await _context.globalSubscriptions.FirstOrDefaultAsync(s => s.UserId == userId && s.IsActive);

        }

        public async Task<User?> GetUserById(int Id)
        {
            return await _context.Users.FindAsync(Id);

        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }

}
