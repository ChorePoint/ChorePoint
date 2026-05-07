using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;

namespace ChorePoint.Application.Handlers.Users.GetUser;

public class GetUserHandler(IAppDbContext context, IUserService userService)
    : IRequestHandler<GetUserQuery, GetUserResponse>
{
    public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var userId = userService.GetUserId();
        if (userId == null)
            throw new UnauthorizedAccessException("User not authorised");

        var user = await context.Users
            .FindAsync(userId, cancellationToken);

        return user.Adapt<GetUserResponse>()
               ?? throw new NotFoundException($"No user exists with id: {userId}");
    }
}