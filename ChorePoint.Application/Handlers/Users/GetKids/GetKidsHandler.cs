using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Users.GetKids;

public class GetKidsHandler(IAppDbContext context, IUserContextService userContextService, IFusionCache cache)
    : IRequestHandler<GetKidsQuery, IReadOnlyCollection<GetKidsResponse>>
{
    public async Task<IReadOnlyCollection<GetKidsResponse>> Handle(GetKidsQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = userContextService.GetParentId();

        var kids = await cache.GetOrSetAsync<IReadOnlyList<User>>(
            $"chore_submissions:{parentId}",
            async _ => await GetKidsAssignedToParentFromDb(parentId, cancellationToken),
            token: cancellationToken
        );

        return kids.Count == 0
            ? throw new NotFoundException($"No kids exist for parent id: {parentId}")
            : kids.Adapt<IReadOnlyCollection<GetKidsResponse>>();
    }

    private async Task<IReadOnlyList<User>> GetKidsAssignedToParentFromDb(int parentId,
        CancellationToken cancellationToken)
    {
        var kids = await context.Users
            .Where(u => u.ParentId == parentId)
            .ToListAsync(cancellationToken);

        return kids;
    }
}