using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Parent.GetKids;

public class GetKidsHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetKidsQuery, IReadOnlyList<GetKidsResponse>>
{
    public async Task<IReadOnlyList<GetKidsResponse>> Handle(GetKidsQuery request, CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var kids = await context.Kids
            .Where(k => k.ParentId.Equals(parentId))
            .ToListAsync(cancellationToken);

        if (kids.Empty())
            throw new NotFoundException($"No kids exist with parent ID [{parentId}]");

        var resourceParentIds = kids.Select(k => k.ParentId).ToList();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        var mapper = new GetKidsMapper();
        return mapper.KidsToGetKidsResponseList(kids);
    }
}