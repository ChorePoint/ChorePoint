using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChorePoint.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ChorePoint.Infrastructure.Authentication;

public partial class JwtTokenGenerator(IConfiguration config, ILogger<JwtTokenGenerator> logger) : IJwtTokenGenerator
{
    public string GenerateJwtToken(int parentId, string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, parentId.ToString()),
            new Claim(ClaimTypes.Email, email)
        };
        LogNewClaimsCreated(parentId);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(config["Jwt:DurationInMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    [LoggerMessage(LogLevel.Information, "New claims created for parent with ID [{ParentId}]")]
    partial void LogNewClaimsCreated(int parentId);
}