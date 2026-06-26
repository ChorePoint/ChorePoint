using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;

namespace ChorePoint.Application.Handlers.Parent.DeleteKid;

public class DeleteKidHandler(IAppDbContext context, IParentContextService parentContextService) : IRequestHandler<DeleteKidCommand>
{
    public async Task Handle(DeleteKidCommand request, CancellationToken cancellationToken)
    {
        var kid = await context.Kids
            .FindAsync([request.KidId], cancellationToken);

        if (kid is null)
            throw new NotFoundException($"No kid exists with ID [{request.KidId}]");

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(kid.ParentId, parentId);

        context.Kids.Remove(kid);
        await context.SaveChangesAsync(cancellationToken);
    }
}