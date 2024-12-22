using library_management_system.Database;
using library_management_system.Database.Entiy;
using library_management_system.DTOs.User;
using library_management_system.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PdfSharp;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace library_management_system.Repositories
{
    public class UserRepo 
    {
       private readonly LibraryDbContext _context;

        public UserRepo(LibraryDbContext context)
        {
            _context = context;
        }




        public async Task<User?> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return await _context.Users.FindAsync(user.Id);
        }




        public async Task<User?> GetUserByEmailOrNic(string emailOrNic)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == emailOrNic || u.UserNic == emailOrNic);
        }

        public async Task<User?>Getuserid(int id)
        {
            return await _context.Users.FindAsync(id);

        }






        public async Task<(List<User> Users, int TotalCount)> GetAllActiveUsers(int pageNumber, int pageSize)
        {
            var subscribers = _context.Users.Where(u => u.IsActive==true);

            if (subscribers == null)
            {
                return (new List<User>(), 0);
            }

            int totalCount = await subscribers.CountAsync();
            var users = await subscribers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }










        public async Task<User> SoftDelete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
               
                throw new Exception("User not found.");
            }          
            user.IsActive = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }





        public async Task<User?> ActivateUserAccount(int id)
        {
           
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id==id);

            if (user == null)
                return null;
              user.IsActive = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserByNICorEmail(string  emailorNic)
        {
            return await _context.Users
         .FirstOrDefaultAsync(u => u.Email == emailorNic || u.UserNic == emailorNic) ;
        }



        public async Task<(List<User> Users, int TotalCount)> GetAllNonactiveUsers(int pageNumber, int pageSize)
        {
            var subscribers = _context.Users.Where(u => u.IsActive==false);

            if (subscribers == null)
            {
                return (new List<User>(), 0);
            }

            int totalCount = await subscribers.CountAsync();
            var users = await subscribers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }





        public async Task<User> DeleteUserPermanently(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return null;
            var log = await _context.LoginPort.FirstOrDefaultAsync(l =>
                l.Email == user.Email || l.NIC == user.UserNic);
            _context.Users.Remove(user);

            if (log != null)
            {
                _context.LoginPort.Remove(log);
            }
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UpdateUser( User updatedUserData)
        {
          _context.Users.Update(updatedUserData);
            _context.SaveChanges();
            return true;
        }






        public async Task<(List<User>, int)> SearchAsync(string searchString, int pageNumber, int pageSize)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(u =>
                    u.FirstName.Contains(searchString) ||
                    u.LastName.Contains(searchString) ||
                    u.Email.Contains(searchString) ||
                    u.PhoneNumber.Contains(searchString));
            }

            int totalRecords = await query.CountAsync();

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalRecords);
        }




        public async Task<List<string>> GetUserEmailsByPrefix(string prefix)
        {

            return await _context.Users
          .Where(u => (EF.Functions.Like(u.Email.ToLower(), prefix.ToLower() + "%")) ||
                      (EF.Functions.Like(u.UserNic.ToLower(), prefix.ToLower() + "%"))) 
          .Select(u => u.Email)
          .ToListAsync();

        }




        public async Task<(List<User> Users, int TotalCount)> GetSubscribedUsersAsync(int pageNumber, int pageSize)
        {
            var subscribers = _context.Users.Where(u => u.IsSubscribed==true);

            if (subscribers == null)
            {
                return (new List<User>(), 0); 
            }

            int totalCount = await subscribers.CountAsync();
            var users = await subscribers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }

        public async Task<OverallUserReportDto> GetUserStatistics()
        {
            int totalUsers = await _context.Users.CountAsync();
            int activeUsers = await _context.Users.CountAsync(u => u.IsActive);
            int nonActiveUsers = await _context.Users.CountAsync(u => !u.IsActive);
            int subscribedUsers = await _context.Users.CountAsync(u => u.IsSubscribed);

            return new OverallUserReportDto
            {
                Created = DateTime.UtcNow,
                TotalUsers = totalUsers,
                ActiveUsers = activeUsers,
                NonActiveUsers = nonActiveUsers,
                SubcribeUsers = subscribedUsers
            };
        }


		//Functions for dashboard

		public async Task<int> GetAllUserCountAsync()
		{
			return await _context.Users.CountAsync(); 
		}

		public async Task<int> GetActiveUserCountAsync()
		{
            var threeMonthsAgo = DateTime.UtcNow.AddMonths(-3);

            return await _context.Users
                .Where(user =>
                    user.IsActive && // User is marked as active
                    (_context.LentRecords.Any(lr => lr.UserId == user.Id) || // Has borrowing records
                     _context.RentHistory.Any(rr => rr.UserId == user.Id && rr.ReturnDate >= threeMonthsAgo)) // Has return activity in the last 3 months
                )
                .CountAsync();
        }
        public async Task<int> GetNonActiveUserCountAsync()
        {
            var threeMonthsAgo = DateTime.Now.AddMonths(-3);

            return await _context.Users
                .Where(user =>
                    !user.IsActive || // User is not marked as active
                    !_context.LentRecords.Any(lr => lr.UserId == user.Id) && // No borrowing records
                    !_context.RentHistory.Any(rr => rr.UserId == user.Id && rr.ReturnDate >= threeMonthsAgo) // No return activity in the last 3 months
                )
                .CountAsync();
        }


        public async Task<int> GetSubscribedUserCountAsync()
		{
			return await _context.Users.CountAsync(user => user.IsSubscribed); // Filter active users
		}

        public async Task<User> GetByid(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

		//DbFunctions for filtering users

		//public async Task<List<Audiobook>> FilterUsersBysubcribedAndbest(int count)
		//{
		//	return await _context.Users
		//		.Include(a => a.Metadata) // Include related Metadata
		//		.OrderByDescending(a => a.AddedDate)
		//		.Take(count) // Get the top 3 audiobooks
		//		.ToListAsync();
		//}

		public async Task<List<User>> FilterUsersBySubscribedAndBest(int count)
		{

            //funcion for subscribed and be

			//return await _context.Users
			//	.Where(u => u.IsSubscribed) 
			//	.OrderByDescending(u => u.RegistrationDate) 
			//	.Take(count) 
			//	.ToListAsync();

            //DbFunctions for oredrerin users

            return await _context.Users
            .Where(u => u.IsActive)
            .OrderByDescending(u => u.RegistrationDate)
            .Take(count)
            .ToListAsync();

        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<int> GetuserCountAsync()
        {
            return await _context.Users.CountAsync();
        }


    }
}
