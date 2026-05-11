using System.Security.Claims;
using ChorePoint.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ChorePoint.Infrastructure.Authentication;

public class UserService(IHttpContextAccessor accessor, ILogger<UserService> logger) : IUserService
{
    public ClaimsPrincipal? GetUser()
    {
        return accessor.HttpContext?.User;
    }

    public int? GetUserId()
    {
        var userId = GetUser()?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            logger.LogError("No user retrieved from HttpContext using name identifier");
            return null;
        }

        return int.Parse(userId);
    }
}