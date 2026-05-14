using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByParent;

public class GetChoresByParentHandler(IAppDbContext context, IUserContextService userContextService, IFusionCache cache)
    : IRequestHandler<GetChoresByParentQuery, IReadOnlyList<GetChoresByParentResponse>>
{
    public async Task<IReadOnlyList<GetChoresByParentResponse>> Handle(GetChoresByParentQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = userContextService.GetParentId();

        var chores = await cache.GetOrSetAsync<IReadOnlyList<Domain.Entities.Chore>>(
            $"chores:{parentId}",
            async _ => await GetChoresByParentFromDb(request, parentId, cancellationToken),
            token: cancellationToken
        );

        if (chores == null || chores.Count == 0)
            throw new NotFoundException($"No chores exist for parent id: {parentId}");

        return chores.Adapt<IReadOnlyList<GetChoresByParentResponse>>();
    }

    private async Task<IReadOnlyList<Domain.Entities.Chore>> GetChoresByParentFromDb(GetChoresByParentQuery request,
        int parentId, CancellationToken cancellationToken)
    {
        var chores = await context.Chores
            .Where(c => c.User.ParentId == parentId)
            .Where(c => c.IsVisible == request.IsVisible)
            .ToListAsync(cancellationToken);

        return chores;
    }
}