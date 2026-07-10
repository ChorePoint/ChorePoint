using MediatR;

namespace ChorePoint.Application.Handlers.Auth.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<LoginResponse>;