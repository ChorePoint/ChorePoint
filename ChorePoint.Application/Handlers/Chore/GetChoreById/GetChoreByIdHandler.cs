using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Chore.GetChoreById;

public class GetChoreByIdHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<GetChoreByIdQuery, GetChoreByIdResponse>
{
    public async Task<GetChoreByIdResponse> Handle(
        GetChoreByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var chore = await context
            .Chores.Include(c => c.Category)
            .Include(c => c.KidChores)
            .SingleOrDefaultAsync(c => c.ChoreId.Equals(request.ChoreId), cancellationToken);

        if (chore is null)
            throw new NotFoundException($"No chore exists with ID [{request.ChoreId}]");

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(chore.ParentId, parentId);

        var mapper = new GetChoreByIdMapper();
        return mapper.ChoreToGetChoreByIdResponse(chore);
    }
}
