using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Chore.Create;

public class CreateChoreHandler(IAppDbContext context, IParentContextService parentContextService, IFusionCache cache)
    : IRequestHandler<CreateChoreCommand>
{
    public async Task Handle(CreateChoreCommand request, CancellationToken cancellationToken)
    {
        var parentId = parentContextService.GetParentId();

        var existingKid = await cache.GetOrSetAsync<Kid?>(
            $"create_chore:{request.KidId}",
            async _ => await GetKidForChoreFromDb(request.KidId, cancellationToken),
            token: cancellationToken
        );

        if (existingKid == null)
            throw new NotFoundException($"No kid exists with ID [{request.KidId}]");

        if (existingKid.ParentId != parentId)
            throw new DomainException(
                $"Kid with assigned parent ID [{existingKid.ParentId}] does not belong to the logged in parent with ID [{parentId}]");

        var chore = new Domain.Entities.Chore
        {
            Name = request.Name,
            Icon = request.Icon,
            Points = request.Points,
            Difficulty = request.Difficulty,
            Frequency = request.Frequency,
            DueDay = request.DueDay,
            KidId = request.KidId,
            Description = request.Description
        };

        context.Chores.Add(chore);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Kid?> GetKidForChoreFromDb(int kidId, CancellationToken cancellationToken)
    {
        return await context.Kids
            .FirstOrDefaultAsync(k => k.Id == kidId, cancellationToken);
    }
}