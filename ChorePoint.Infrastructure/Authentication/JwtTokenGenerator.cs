using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChorePoint.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ChorePoint.Infrastructure.Authentication;

public partial class JwtTokenGenerator(ILogger<JwtTokenGenerator> logger) : IJwtTokenGenerator
{
    public string GenerateJwtToken(int parentId, string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, parentId.ToString()),
            new Claim(ClaimTypes.Email, email),
        };
        LogNewClaimsCreated(parentId);

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!)
        );
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            Environment.GetEnvironmentVariable("JWT_ISSUER"),
            Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            claims,
            expires: DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(Environment.GetEnvironmentVariable("JWT_DURATION"))
            ),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [LoggerMessage(LogLevel.Information, "New claims created for parent with ID [{ParentId}]")]
    partial void LogNewClaimsCreated(int parentId);
}
