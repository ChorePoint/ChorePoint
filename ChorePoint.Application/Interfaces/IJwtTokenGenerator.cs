namespace ChorePoint.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(int id, string email);
}