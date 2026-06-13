using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Parent.GetKids;

public class GetKidsHandler(IAppDbContext context, IParentContextService parentContextService, IFusionCache cache)
    : IRequestHandler<GetKidsQuery, IReadOnlyList<GetKidsResponse>>
{
    public async Task<IReadOnlyList<GetKidsResponse>> Handle(GetKidsQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var kids = await cache.GetOrSetAsync(
            $"get_kids:{parentId}",
            async _ => await GetKidsAssignedToParentFromDb(parentId, cancellationToken),
            token: cancellationToken
        );

        return kids.Empty()
            ? throw new NotFoundException($"No kids exist with parent ID [{parentId}]")
            : kids.Adapt<IReadOnlyList<GetKidsResponse>>();
    }

    private async Task<IReadOnlyList<Kid>> GetKidsAssignedToParentFromDb(int parentId,
        CancellationToken cancellationToken)
    {
        return await context.Kids
            .Where(k => k.ParentId == parentId)
            .ToListAsync(cancellationToken);
    }
}