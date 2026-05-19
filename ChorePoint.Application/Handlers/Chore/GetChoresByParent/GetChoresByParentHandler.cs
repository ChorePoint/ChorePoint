using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByParent;

public class GetChoresByParentHandler(IAppDbContext context, IParentContextService parentContextService, IFusionCache cache)
    : IRequestHandler<GetChoresByParentQuery, IReadOnlyList<GetChoresByParentResponse>>
{
    public async Task<IReadOnlyList<GetChoresByParentResponse>> Handle(GetChoresByParentQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var chores = await cache.GetOrSetAsync<IReadOnlyList<Domain.Entities.Chore>>(
            $"get_chores_by_parent:{parentId}:{request.IsVisible}",
            async _ => await GetChoresByParentFromDb(parentId, request.IsVisible, cancellationToken),
            token: cancellationToken
        );

        if (chores == null || chores.Count == 0)
            throw new NotFoundException($"No chores exist for parent ID [{parentId}]");

        return chores.Adapt<IReadOnlyList<GetChoresByParentResponse>>();
    }

    private async Task<IReadOnlyList<Domain.Entities.Chore>> GetChoresByParentFromDb(int parentId, bool isVisible,
        CancellationToken cancellationToken)
    {
        return await context.Chores
            .Where(c => c.Kid.ParentId == parentId)
            .Where(c => c.IsVisible == isVisible)
            .ToListAsync(cancellationToken);
    }
}