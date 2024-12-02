using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using library_management_system.Database.Entiy;
using Microsoft.IdentityModel.Tokens;


namespace library_management_system.Utilities
{
    public class JwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiresInMinutes;

        public JwtService(IConfiguration configuration)
        {
            _secretKey = configuration["JwtSettings:SecretKey"];
            _issuer = configuration["JwtSettings:Issuer"];
            _audience = configuration["JwtSettings:Audience"];
            _expiresInMinutes = int.Parse(configuration["JwtSettings:ExpiresInMinutes"]);
        }


        public string GenerateToken(User user)
        {
            var claimsList = new List<Claim>
    {
        new Claim("FullName", user.FullName),
         new Claim("ID", user.Id.ToString()),
        new Claim("Email", user.Email),
        new Claim("role", "user"),
        new Claim("IsSubscribed", user.IsSubscribed.ToString())
    };

            if (!string.IsNullOrEmpty(user.UserNic))
            {
                claimsList.Add(new Claim("UserNic", user.UserNic));
            }

            // Optional claims based on user properties
            claimsList.Add(new Claim("IsActive", user.IsActive.ToString()));
            claimsList.Add(new Claim("IsSubscribed", user.IsSubscribed.ToString()));

            // Create security key and signing credentials
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate token with specified properties
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claimsList,
                expires: DateTime.UtcNow.AddMinutes(_expiresInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string GenerateAdminToken(Admin admin)
        {

            var claimsList = new List<Claim>
    {
        new Claim("FullName", admin.FullName),
        new Claim("Email", admin.Email),
        new Claim("role", "Admin"), 
        new Claim("IsMaster", admin.IsMaster.ToString()),
        new Claim("ID", admin.id.ToString()),
    
        
    };

            if (!string.IsNullOrEmpty(admin.AdminNic))
            {
                claimsList.Add(new Claim("AdminNic", admin.AdminNic));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claimsList,
                expires: DateTime.UtcNow.AddMinutes(_expiresInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
