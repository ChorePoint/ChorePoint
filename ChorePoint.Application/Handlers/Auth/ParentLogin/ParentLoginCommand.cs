using MediatR;

namespace ChorePoint.Application.Handlers.Auth.ParentLogin;

public record ParentLoginCommand(string Email, string Password) : IRequest<ParentLoginResponse>;
