using library_management_system.Database;
using library_management_system.Database.Entiy;
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

    }
}
