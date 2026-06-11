using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.CreateChore;

public class CreateChoreHandler(IAppDbContext context, IParentContextService parentContextService, IFusionCache cache)
    : IRequestHandler<CreateChoreCommand>
{
    public async Task Handle(CreateChoreCommand request, CancellationToken cancellationToken)
    {
        var existingKid = await cache.GetOrSetAsync<Kid?>(
            $"create_chore:{request.KidId}",
            async _ => await GetKidForChoreFromDb(request.KidId, cancellationToken),
            token: cancellationToken
        );

        if (existingKid is null)
            throw new NotFoundException($"No kid exists with ID [{request.KidId}]");

        var parentId = parentContextService.GetParentId();

        if (existingKid.ParentId != parentId)
            throw new DomainException(
                $"Kid with assigned parent ID [{existingKid.ParentId}] does not belong to the logged in parent with ID [{parentId}]");

        var chore = ChoreE.Create
        (
            request.Name,
            request.Icon,
            request.Points,
            request.Difficulty,
            request.Frequency,
            request.DueDay,
            request.KidId,
            request.Description,
            DateTime.UtcNow
        );

        context.Chores.Add(chore);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Kid?> GetKidForChoreFromDb(int kidId, CancellationToken cancellationToken)
    {
        return await context.Kids
            .FirstOrDefaultAsync(k => k.Id == kidId, cancellationToken);
    }
}