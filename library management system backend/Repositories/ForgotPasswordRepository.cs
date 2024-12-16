using library_management_system.Database.Entiy;
using library_management_system.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class ForgotPasswordRepository
{
    private readonly LibraryDbContext _context;

    public ForgotPasswordRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<ForgotPasswordToken> SaveTokenAsync(string email, string tokenCode)
    {
        var token = new ForgotPasswordToken
        {
            Email = email,
            TokenCode = tokenCode,
            CreatedAt = DateTime.UtcNow
        };

        _context.ForgotPasswordTokens.Add(token);
        await _context.SaveChangesAsync();
        return token;
    }

    public async Task<ForgotPasswordToken?> GetLatestTokenAsync(string email)
    {
        return await _context.ForgotPasswordTokens
            .Where(t => t.Email == email)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteTokenAsync(ForgotPasswordToken token)
    {
        _context.ForgotPasswordTokens.Remove(token);
        await _context.SaveChangesAsync();
    }


    public async Task<bool> UpdatePasswordAsync(string email, string password)
    {
        try
        {
            var data = await _context.LoginPort.FirstOrDefaultAsync(u => u.Email == email);
            if (data == null)
            {

                return false;
            }

            data.PasswordHash = password;  
            _context.LoginPort.Update(data);
            await _context.SaveChangesAsync(); 

            return true;
        }
        catch (Exception ex)
        {

            return false; 
        }
    }
}
