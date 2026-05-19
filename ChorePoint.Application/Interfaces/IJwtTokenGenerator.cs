namespace ChorePoint.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(int parentId, string email);
}