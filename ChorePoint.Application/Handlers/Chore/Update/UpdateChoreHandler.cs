using ChorePoint.Application.Handlers.Chore.Update;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.Create;

public class UpdateChoreHandler(IAppDbContext context, IParentContextService parentContextService, IFusionCache cache)
    : IRequestHandler<UpdateChoreCommand>
{
    public async Task Handle(UpdateChoreCommand request, CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var existingChore = await GetChoreFromDb(request.Id, parentId, cancellationToken);

        if (existingChore == null)
            throw new NotFoundException($"No chore exists with ID [{request.Id}]");

        existingChore.KidId = request.KidId;
        existingChore.Name = request.Name;
        existingChore.Icon = request.Icon;
        existingChore.Points = request.Points;
        existingChore.Difficulty = request.Difficulty;
        existingChore.Frequency = request.Frequency;
        existingChore.IsVisible = request.IsVisible;
        existingChore.Description = request.Description;
        existingChore.DueDay = request.DueDay;

        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<ChoreE?> GetChoreFromDb(int choreId, int parentId, CancellationToken cancellationToken)
    {
        return await context.Chores
            .Where(c => c.Kid.ParentId == parentId)
            .FirstOrDefaultAsync(c => c.Id == choreId, cancellationToken);
    }
}