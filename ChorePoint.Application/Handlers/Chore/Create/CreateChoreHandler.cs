using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace ChorePoint.Application.Handlers.Chore.Create;

public class CreateChoreHandler(IAppDbContext context, IUserContextService userContextService, IFusionCache cache)
    : IRequestHandler<CreateChoreCommand>
{
    public async Task Handle(CreateChoreCommand request, CancellationToken cancellationToken)
    {
        var parentId = userContextService.GetParentId();

        var existingKid = await cache.GetOrSetAsync<User?>(
            $"kid:{request.KidId}",
            async _ => await GetKidForChoreFromDb(request, cancellationToken),
            token: cancellationToken
        );

        if (existingKid == null)
            throw new NotFoundException($"No kid exists for this kid id: {request.KidId}");

        if (existingKid.ParentId != parentId)
            throw new DomainException($"Kid ID does not belong to the current parent: {parentId}");

        var chore = new Domain.Entities.Chore
        {
            Name = request.Name,
            Icon = request.Icon,
            Points = request.Points,
            Difficulty = request.Difficulty,
            Frequency = request.Frequency,
            DueDay = request.DueDay,
            UserId = request.KidId,
            Description = request.Description
        };

        context.Chores.Add(chore);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task<User?> GetKidForChoreFromDb(CreateChoreCommand request, CancellationToken cancellationToken)
    {
        var existingKid = await context.Users
            .FirstOrDefaultAsync(u => u.Id == request.KidId, cancellationToken);

        return existingKid;
    }
}