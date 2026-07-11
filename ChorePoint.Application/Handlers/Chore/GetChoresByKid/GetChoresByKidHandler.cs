using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByKid;

public class GetChoresByKidHandler(
    IAppDbContext context,
    IParentContextService parentContextService
) : IRequestHandler<GetChoresByKidQuery, IReadOnlyList<GetChoresByKidResponse>>
{
    public async Task<IReadOnlyList<GetChoresByKidResponse>> Handle(
        GetChoresByKidQuery request,
        CancellationToken cancellationToken
    )
    {
        var chores = await context
            .Chores.Include(c => c.Category)
            .Include(c => c.KidChores.Where(kc => kc.KidId.Equals(request.KidId)))
            .Where(c => c.KidChores.Any(kc => kc.KidId.Equals(request.KidId)))
            .ToListAsync(cancellationToken);

        if (chores.Empty())
            throw new NotFoundException($"No chores exist with kid ID [{request.KidId}]");

        var resourceParentIds = chores.Select(c => c.ParentId).ToList();
        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        var mapper = new GetChoresByKidMapper();
        return mapper.ChoresToGetChoresByKidResponseList(chores);
    }
}
