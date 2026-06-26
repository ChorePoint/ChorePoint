using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Parent.GetKids;

public class GetKidsHandler(IAppDbContext context, IParentContextService parentContextService) : IRequestHandler<GetKidsQuery, IReadOnlyList<GetKidsResponse>>
{
    public async Task<IReadOnlyList<GetKidsResponse>> Handle(GetKidsQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var kids = await context.Kids
            .Include(k => k.Parent)
            .Include(k => k.Chores)
            .Where(k => k.ParentId.Equals(parentId))
            .ToListAsync(cancellationToken);

        return kids.Empty()
            ? throw new NotFoundException($"No kids exist with parent ID [{parentId}]")
            : kids.Adapt<IReadOnlyList<GetKidsResponse>>();
    }
}