using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using ChorePoint.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Parent.GetKidsByParent;

public class GetKidsByParentHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetKidsByParentQuery, IReadOnlyList<GetKidsByParentResponse>>
{
    public async Task<IReadOnlyList<GetKidsByParentResponse>> Handle(GetKidsByParentQuery request,
        CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var kids = await context.Kids
            .Where(k => k.ParentId.Equals(parentId))
            .ToListAsync(cancellationToken);

        if (kids.Empty())
            throw new NotFoundException($"No kids exist with parent ID [{parentId}]");

        var resourceParentIds = kids.Select(k => k.ParentId).ToList();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        var mapper = new GetKidsByParentMapper();
        return mapper.KidsToGetKidsByParentResponseList(kids);
    }
}