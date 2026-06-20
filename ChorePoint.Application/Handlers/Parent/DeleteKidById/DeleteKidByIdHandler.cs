using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;

namespace ChorePoint.Application.Handlers.Parent.DeleteKidById;

public class DeleteKidByIdHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<DeleteKidByIdCommand>
{
    public async Task Handle(DeleteKidByIdCommand request, CancellationToken cancellationToken)
    {
        var kid = await GetKidByIdFromDb(request.KidId, cancellationToken);

        if (kid is null)
            throw new NotFoundException($"No kid exists with ID [{request.KidId}]");

        var parentId = parentContextService.GetParentId();

        if (kid.ParentId != parentId)
            throw new DomainException(
                $"Kid with assigned parent ID [{kid.ParentId}] does not belong to the logged in parent with ID [{parentId}]");

        context.Kids.Remove(kid);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Kid?> GetKidByIdFromDb(int kidId, CancellationToken cancellationToken)
    {
        return await context.Kids
            .FindAsync([kidId], cancellationToken);
    }
}