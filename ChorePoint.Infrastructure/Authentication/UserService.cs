using System.Security.Claims;
using ChorePoint.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ChorePoint.Infrastructure.Authentication;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor accessor;

    public UserService(IHttpContextAccessor accessor)
    {
        this.accessor = accessor;
    }

    public ClaimsPrincipal? GetUser()
    {
        return accessor?.HttpContext?.User;
    }

    public int? GetUserId()
    {
        var userId = GetUser()?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return null;
        
        return int.Parse(userId);
    }
}