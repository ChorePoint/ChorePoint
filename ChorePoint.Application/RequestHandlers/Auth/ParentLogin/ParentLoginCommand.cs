using MediatR;

namespace ChorePoint.Application.RequestHandlers.Auth.ParentLogin;

public record ParentLoginCommand(string Email, string Password) : IRequest<ParentLoginResponse>;
