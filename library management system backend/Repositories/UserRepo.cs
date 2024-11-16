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

        public async Task<User> CreateUser(User user)
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

        public async Task<User>Getuserid(int id)
        {
            return await _context.Users
               .FirstOrDefaultAsync(u => u.Id == id );
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
        public async Task<User> GetUserByNICorEmail(string  emailorNic)
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
        public async Task<User> UpdateUser(int id, User updatedUserData)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (existingUser == null)
                return null;

            // Update only the necessary fields
           
            existingUser.FirstName = updatedUserData.FirstName;
            existingUser.LastName = updatedUserData.LastName;
            existingUser.FullName = updatedUserData.FullName;
            
            existingUser.PhoneNumber = updatedUserData.PhoneNumber;
            existingUser.Address = updatedUserData.Address;
            existingUser.ProfileImage = updatedUserData.ProfileImage;
           

            // Save changes to the database
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return existingUser;
        }

    }
}
