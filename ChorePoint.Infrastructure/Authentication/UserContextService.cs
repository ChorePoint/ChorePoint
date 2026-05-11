using System.Security.Claims;
using ChorePoint.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ChorePoint.Infrastructure.Authentication;

public class UserContextService(IHttpContextAccessor accessor) : IUserContextService
{
    public int GetParentId()
    {
        var userId = GetParent()?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new UnauthorizedAccessException("User not authorised!");

        return int.Parse(userId);
    }

    private ClaimsPrincipal? GetParent()
    {
        return accessor.HttpContext?.User;
    }
}