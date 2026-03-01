using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"] ?? throw new InvalidOperationException("JWT Key is missing!")));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenDescriptor);
        }
    }
}