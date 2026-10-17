using System.Security.Claims;

using ChorePoint.Application.Interfaces;

using Microsoft.AspNetCore.Http;

namespace ChorePoint.Infrastructure.Authentication;

public class KidContextService(IHttpContextAccessor accessor) : IKidContextService
{
    public bool IsKid()
    {
        return GetKid()?.IsInRole(AuthenticationConstants.KidRole)
               ?? throw new UnauthorizedAccessException("Kid is not authorised!");
    }

    public int GetKidId()
    {
        var kidId = GetKid()?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (kidId is null)
        {
            throw new UnauthorizedAccessException("Kid is not authorised!");
        }

        return int.Parse(kidId);
    }

    private ClaimsPrincipal? GetKid()
    {
        return accessor.HttpContext?.User;
    }
}
