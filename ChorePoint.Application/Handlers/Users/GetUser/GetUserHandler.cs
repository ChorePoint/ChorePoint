using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;

namespace ChorePoint.Application.Handlers.Users.GetUser;

public class GetUserHandler(IAppDbContext context, IUserContextService userContextService)
    : IRequestHandler<GetUserQuery, GetUserResponse>
{
    public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var userId = userContextService.GetParentId();

        var user = await context.Parents
            .FindAsync(userId, cancellationToken);

        return user.Adapt<GetUserResponse>()
               ?? throw new NotFoundException($"No user exists with id: {userId}");
    }
}