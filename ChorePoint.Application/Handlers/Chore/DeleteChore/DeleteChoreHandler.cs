using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;

namespace ChorePoint.Application.Handlers.Chore.DeleteChore;

public class DeleteChoreHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<DeleteChoreCommand>
{
    public async Task Handle(DeleteChoreCommand request, CancellationToken cancellationToken)
    {
        var chore = await context.Chores.FindAsync([request.ChoreId], cancellationToken);

        if (chore is null)
            throw new NotFoundException($"No chore exists with ID [{request.ChoreId}]");

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(chore.ParentId, parentId);

        context.Chores.Remove(chore);
        await context.SaveChangesAsync(cancellationToken);
    }
}
