using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Users.GetUser;

public class GetUserHandler(IAppDbContext context, IUserContextService userContextService, IFusionCache cache)
    : IRequestHandler<GetUserQuery, GetUserResponse>
{
    public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var parentId = userContextService.GetParentId();

        var parent = await cache.GetOrSetAsync<Parent?>(
            $"parent:{parentId}",
            async _ => await GetParentDetailsFromDb(parentId, cancellationToken),
            token: cancellationToken
        );

        return parent.Adapt<GetUserResponse>()
               ?? throw new NotFoundException($"No parent exists with id: {parentId}");
    }

    private async Task<Parent?> GetParentDetailsFromDb(int userId, CancellationToken cancellationToken)
    {
        var user = await context.Parents
            .FindAsync([userId], cancellationToken);

        return user;
    }
}