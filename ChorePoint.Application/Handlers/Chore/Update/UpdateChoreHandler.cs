using ChorePoint.Application.Handlers.Chore.Update;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.Create;

public class UpdateChoreHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<UpdateChoreCommand>
{
    public async Task Handle(UpdateChoreCommand request, CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var existingChore = await GetChoreFromDb(request.Id, parentId, cancellationToken);

        if (existingChore is null)
            throw new NotFoundException($"No chore exists with ID [{request.Id}]");

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

    private async Task<ChoreE?> GetChoreFromDb(int choreId, int parentId, CancellationToken cancellationToken)
    {
        return await context.Chores
            .Where(c => c.Kid.ParentId == parentId)
            .FirstOrDefaultAsync(c => c.Id == choreId, cancellationToken);
    }
}