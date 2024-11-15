﻿using library_management_system.Database;
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


    }
}
