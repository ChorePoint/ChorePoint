using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.DeleteChoreById;
public class DeleteChoreByIdHandler(IAppDbContext context, IParentContextService parentContextService, IFusionCache cache)
    : IRequestHandler<DeleteChoreByIdCommand>
{
    public async Task Handle(DeleteChoreByIdCommand request, CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var chore = await GetChoreByIdFromDb(request.Id, cancellationToken);

        if (chore == null)
            throw new NotFoundException($"No chore exists with ID [{request.Id}]");

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