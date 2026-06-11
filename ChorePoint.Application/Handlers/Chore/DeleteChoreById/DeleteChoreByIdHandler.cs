using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.DeleteChoreById;

public class DeleteChoreByIdHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<DeleteChoreByIdCommand>
{
    public async Task Handle(DeleteChoreByIdCommand request, CancellationToken cancellationToken)
    {
        var chore = await GetChoreByIdFromDb(request.ChoreId, cancellationToken);

        if (chore is null)
            throw new NotFoundException($"No chore exists with ID [{request.ChoreId}]");

        var parentId = parentContextService.GetParentId();

        if (chore.Kid.ParentId != parentId)
            throw new DomainException(
                $"Chore with assigned parent ID [{chore.Kid.ParentId}] does not belong to the logged in parent with ID [{parentId}]");

        context.Chores.Remove(chore);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<ChoreE?> GetChoreByIdFromDb(int choreId, CancellationToken cancellationToken)
    {
        return await context.Chores
            .Include(c => c.Kid)
            .FirstOrDefaultAsync(c => c.Id == choreId, cancellationToken);
    }
}