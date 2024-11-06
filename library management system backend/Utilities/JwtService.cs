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
            var claimsList = new List<Claim>();
            claimsList.Add(new Claim("NIC", user.NIC));
            claimsList.Add(new Claim("Name", user.Name));
            claimsList.Add(new Claim("Email", user.Email));

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: _issuer,
              audience: _audience,
              claims: claimsList,
              expires: DateTime.Now.AddMinutes(_expiresInMinutes),
              signingCredentials: credentials
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
