using MediatR;

namespace ChorePoint.Application.Handlers.Auth.KidLogin;

public record KidLoginCommand(string LoginCode) : IRequest<KidLoginResponse>;
