using MediatR;

namespace ChorePoint.Application.Handlers.Auth.KidLogin;

public record KidLoginCommand(string Name, int LoginCode) : IRequest<KidLoginResponse>;
