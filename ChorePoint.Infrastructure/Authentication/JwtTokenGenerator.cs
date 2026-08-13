using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using ChorePoint.Application.Interfaces;

using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ChorePoint.Infrastructure.Authentication;

public partial class JwtTokenGenerator(ILogger<JwtTokenGenerator> logger) : IJwtTokenGenerator
{
    public string GenerateKidJwtToken(int parentId, string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, parentId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, JwtConstants.KidRole)
        };
        LogNewClaimsCreated(parentId, JwtConstants.KidRole);

        return GenerateJwtToken(claims, "JWT_KID_DURATION");
    }

    public string GenerateParentJwtToken(int parentId, string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, parentId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, JwtConstants.ParentRole),
        };
        LogNewClaimsCreated(parentId, JwtConstants.ParentRole);

        return GenerateJwtToken(claims);
    }

    private static string GenerateJwtToken(Claim[] claims, string durationEnvVar = "JWT_DURATION")
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            Environment.GetEnvironmentVariable("JWT_ISSUER"),
            Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(Environment.GetEnvironmentVariable(durationEnvVar))),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [LoggerMessage(LogLevel.Information, "New claims created using parent ID [{ParentId}] with role [{Role}]")]
    partial void LogNewClaimsCreated(int parentId, string role);
}
