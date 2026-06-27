using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByParent;

public class GetChoresByParentHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetChoresByParentQuery, IReadOnlyList<GetChoresByParentResponse>>
{
    public async Task<IReadOnlyList<GetChoresByParentResponse>> Handle(GetChoresByParentQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var chores = await context.Chores
            .Include(c => c.Category)
            .Include(c => c.KidChores)
            .Where(c => c.ParentId.Equals(parentId))
            .Where(c => request.IsVisible == null || c.KidChores.Any(kc => kc.IsVisible.Equals(request.IsVisible)))
            .ToListAsync(cancellationToken);

        if (chores.Empty())
            throw new NotFoundException($"No chores exist for parent ID [{parentId}]");

        var resourceParentIds = chores.Select(c => c.ParentId).ToList();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        var mapper = new GetChoresByParentMapper();
        return mapper.ChoresToGetChoresByParentResponseList(chores);
    }
}