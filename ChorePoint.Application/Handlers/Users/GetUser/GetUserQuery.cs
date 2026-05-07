using MediatR;

namespace ChorePoint.Application.Handlers.Users.GetUser;

public record GetUserQuery : IRequest<GetUserResponse>;