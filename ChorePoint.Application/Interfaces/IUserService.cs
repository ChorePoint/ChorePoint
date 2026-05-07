using System.Security.Claims;

namespace ChorePoint.Application.Interfaces;

public interface IUserService
{
    ClaimsPrincipal? GetUser();
    int? GetUserId();
}