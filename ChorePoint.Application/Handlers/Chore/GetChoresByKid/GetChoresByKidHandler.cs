using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByKid;

public class GetChoresByKidHandler(IAppDbContext context, IParentContextService parentContextService) : IRequestHandler<GetChoresByKidQuery, IReadOnlyList<GetChoresByKidResponse>>
{
    public async Task<IReadOnlyList<GetChoresByKidResponse>> Handle(GetChoresByKidQuery request,
        CancellationToken cancellationToken)
    {
        var chores = await context.Chores
            .Include(c => c.KidChores)
            .Where(c => c.KidChores.Any(kc => kc.KidId.Equals(request.KidId)))
            .ToListAsync(cancellationToken);

        if (chores.Empty())
            throw new NotFoundException($"No chores exist with kid ID [{request.KidId}]");
        
        var resourceParentIds = chores.Select(chore => chore.ParentId).ToList();
        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        return chores.Adapt<IReadOnlyList<GetChoresByKidResponse>>();
    }
}