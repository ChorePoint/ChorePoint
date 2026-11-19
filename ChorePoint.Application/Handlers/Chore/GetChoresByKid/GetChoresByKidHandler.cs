using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByKid;

public class GetChoresByKidHandler(IAppDbContext context, IParentContextService parentContextService, IKidContextService kidContextService)
    : IRequestHandler<GetChoresByKidQuery, IReadOnlyList<GetChoresByKidResponse>>
{
    public async Task<IReadOnlyList<GetChoresByKidResponse>> Handle(GetChoresByKidQuery request, CancellationToken cancellationToken)
    {
        var kidId = request.KidId ?? kidContextService.GetKidId();

        var chores = await context.Chores
            .Include(c => c.Category)
            .Include(c => c.KidChores.Where(kc => kc.KidId.Equals(kidId)))
            .Where(c => c.KidChores.Any(kc => kc.KidId.Equals(kidId)))
            .ToListAsync(cancellationToken);

        if (chores.Empty())
        {
            throw new NotFoundException($"No chores exist with kid ID [{kidId}]");
        }

        var resourceParentIds = chores.Select(c => c.ParentId).ToList();
        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        GetChoresByKidMapper mapper = new();
        return mapper.ChoresToGetChoresByKidResponseList(chores);
    }
}
