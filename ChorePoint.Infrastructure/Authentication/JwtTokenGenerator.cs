using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using ChorePoint.Application.Interfaces;
using ChorePoint.Infrastructure.Options;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChorePoint.Infrastructure.Authentication;

public partial class JwtTokenGenerator(IOptions<AuthenticationOptions> authOptions, ILogger<JwtTokenGenerator> logger) : IJwtTokenGenerator
{
    private readonly AuthenticationOptions _authOptions = authOptions.Value;

    public string GenerateKidJwtToken(int parentId, string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, parentId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, AuthenticationConstants.KidRole)
        };
        LogNewClaimsCreated(parentId, AuthenticationConstants.KidRole);

        return GenerateJwtToken(claims, _authOptions.JwtKidDuration);
    }

    public string GenerateParentJwtToken(int parentId, string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, parentId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, AuthenticationConstants.ParentRole),
        };
        LogNewClaimsCreated(parentId, AuthenticationConstants.ParentRole);

        return GenerateJwtToken(claims, _authOptions.JwtDuration);
    }

    private string GenerateJwtToken(Claim[] claims, double jwtDuration)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_authOptions.JwtKey));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            _authOptions.JwtIssuer,
            _authOptions.JwtAudience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(jwtDuration),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [LoggerMessage(LogLevel.Information, "New claims created using parent ID [{ParentId}] with role [{Role}]")]
    partial void LogNewClaimsCreated(int parentId, string role);
}
