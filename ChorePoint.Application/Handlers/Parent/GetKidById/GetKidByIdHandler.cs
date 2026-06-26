using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Parent.GetKidById;

public class GetKidByIdHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetKidByIdQuery, GetKidByIdResponse>
{
    public async Task<GetKidByIdResponse> Handle(GetKidByIdQuery request, CancellationToken cancellationToken)
    {
        var kid = await context.Kids
            .Include(k => k.Parent)
            .Include(c => c.Chores)
            .SingleOrDefaultAsync(k => k.KidId.Equals(request.KidId), cancellationToken);

        if (kid is null)
            throw new NotFoundException($"No kid exists with ID [{request.KidId}]");

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(kid.ParentId, parentId);

        return kid.Adapt<GetKidByIdResponse>();
    }
}