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

    // Save the OTP to the database
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

    // Retrieve the latest OTP for the given email
    public async Task<ForgotPasswordToken?> GetLatestTokenAsync(string email)
    {
        return await _context.ForgotPasswordTokens
            .Where(t => t.Email == email)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync();
    }

    // Delete the OTP from the database
    public async Task DeleteTokenAsync(ForgotPasswordToken token)
    {
        _context.ForgotPasswordTokens.Remove(token);
        await _context.SaveChangesAsync();
    }
}
