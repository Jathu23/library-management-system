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
        public async Task<LoginT?> GetByEmailOrNic(string emailOrNic)
        {
            return await _context.LoginPort.FirstOrDefaultAsync(a => a.Email == emailOrNic || a.NIC == emailOrNic);
        }
    }
}
