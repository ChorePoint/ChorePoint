using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.UpdateChore;

public class UpdateChoreHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<UpdateChoreCommand>
{
    public async Task Handle(UpdateChoreCommand request, CancellationToken cancellationToken)
    {
        var existingChore = await GetChoreByIdFromDb(request.Id, cancellationToken);

        if (existingChore is null)
            throw new NotFoundException($"No chore exists with ID [{request.Id}]");

        var parentId = parentContextService.GetParentId();

        if (existingChore.Kid.ParentId != parentId)
            throw new DomainException(
                $"Chore with assigned parent ID [{existingChore.Kid.ParentId}] does not belong to the logged in parent with ID [{parentId}]");

        existingChore.Update(request.KidId,
            request.Name,
            request.Icon,
            request.Points,
            request.Difficulty,
            request.Frequency,
            request.IsVisible,
            request.Description,
            request.DueDay);

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<ChoreE?> GetChoreByIdFromDb(int choreId, CancellationToken cancellationToken)
    {
        return await context.Chores
            .Include(c => c.Kid)
            .Where(c => c.Id == choreId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}