using System.Security.Claims;
using ChorePoint.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ChorePoint.Infrastructure.Authentication;

public class ParentContextService(IHttpContextAccessor accessor) : IParentContextService
{
    public int GetParentId()
    {
        var parentId = GetParent()?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (parentId is null)
            throw new UnauthorizedAccessException("Parent is not authorised!");

        return int.Parse(parentId);
    }

    private ClaimsPrincipal? GetParent()
    {
        return accessor.HttpContext?.User;
    }
}