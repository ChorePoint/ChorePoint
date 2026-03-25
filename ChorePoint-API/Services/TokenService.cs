using ChorePoint_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChorePoint_API.Services
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
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:DurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
