using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class AdminRepo
    {
        private readonly LibraryDbContext _context;

        public AdminRepo(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Admin> CreateAdmin(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return await _context.Admins.FindAsync(admin.id);
        }

        public async Task<Admin> GetAdminByEmailOrNic(string emailOrNic)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == emailOrNic || a.AdminNic == emailOrNic);
        }
    }
}
