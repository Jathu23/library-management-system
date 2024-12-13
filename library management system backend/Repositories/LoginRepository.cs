using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class LoginRepository
    {
        private readonly LibraryDbContext _context;

        public LoginRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Addlogdata(LoginT data)
        {
            try
            {
                _context.LoginPort.Add(data);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        
        }

        public async Task<bool> UpdateUserLoginData(string email, string newNic)
        {

            var userdata = await _context.LoginPort.FirstOrDefaultAsync(a => a.Email == email);

            userdata.NIC = newNic;

         _context.LoginPort.Update(userdata);

            var changes = await _context.SaveChangesAsync();

            
            if (changes == 0)
            {
                return false; 
            }
            return true;
        }


        public async Task<LoginT?> GetByEmailOrNic(string emailOrNic)
        {
            return await _context.LoginPort.FirstOrDefaultAsync(a => a.Email == emailOrNic || a.NIC == emailOrNic);
        }
        public async Task<User?> GetUserByEmailOrNic(string emailOrNic)
        {
            var iaActive= await _context.Users
                .FirstOrDefaultAsync(u => u.Email == emailOrNic || u.UserNic == emailOrNic ) ;
            if (iaActive != null && iaActive.IsActive == true)

            {
                return iaActive;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> IsNicAvailable(string nic)
        {
            
            return await _context.Users.AnyAsync(u => u.UserNic == nic);
        }

    }   
}
