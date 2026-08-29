namespace ChorePoint.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateKidJwtToken(int parentId, string email);
    string GenerateParentJwtToken(int parentId, string email);
}
