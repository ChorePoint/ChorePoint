using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Parent.GetKids;

public class GetKidsHandler(IAppDbContext context, IParentContextService parentContextService, IFusionCache cache)
    : IRequestHandler<GetKidsQuery, IReadOnlyCollection<GetKidsResponse>>
{
    public async Task<IReadOnlyCollection<GetKidsResponse>> Handle(GetKidsQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var kids = await cache.GetOrSetAsync<IReadOnlyList<Kid>>(
            $"get_kids:{parentId}",
            async _ => await GetKidsAssignedToParentFromDb(parentId, cancellationToken),
            token: cancellationToken
        );

        return kids.Count == 0
            ? throw new NotFoundException($"No kids exist with parent ID [{parentId}]")
            : kids.Adapt<IReadOnlyCollection<GetKidsResponse>>();
    }

    private async Task<IReadOnlyList<Kid>> GetKidsAssignedToParentFromDb(int parentId,
        CancellationToken cancellationToken)
    {
        return await context.Kids
            .Where(k => k.ParentId == parentId)
            .ToListAsync(cancellationToken);
    }
}