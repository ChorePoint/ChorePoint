using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChorePoint.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace ChorePoint.API.Services
{
    public class TokenService
    {
        private readonly string _key = "supersecretkey";
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(Parent parent)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, parent.Id.ToString()),
                new Claim(ClaimTypes.Email, parent.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:DurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
