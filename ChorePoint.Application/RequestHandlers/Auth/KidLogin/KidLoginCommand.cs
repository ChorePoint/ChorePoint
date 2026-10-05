using MediatR;

namespace ChorePoint.Application.RequestHandlers.Auth.KidLogin;

public record KidLoginCommand(string LoginCode) : IRequest<KidLoginResponse>;
