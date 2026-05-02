namespace ChorePoint.Application.Handlers.Auth.Login;

public class LoginResponse
{
    public record LoginUserResponse(
        string Token
    );
}