using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<User>> GetAllUsers()
        {

            var data = await _context.Users
                               .Where(u => u.IsActive)  
                               .ToListAsync();
            return data;
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
        public async Task<User?> GetUserByNICorEmail(string  emailorNic)
        {
            return await _context.Users
         .FirstOrDefaultAsync(u => u.Email == emailorNic || u.UserNic == emailorNic) ;
        }
        public async Task<List<User>> GetAllDisabeledUsers()
        {
            var users=await _context.Users.Where(u => u.IsActive==false).ToListAsync();
            return users;
        }


        public async Task<User> DeleteUserPermanently(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return null;

            _context.Users.Remove(user);
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
          .Select(u => u.FullName)
          .ToListAsync();

        }

    }
}
