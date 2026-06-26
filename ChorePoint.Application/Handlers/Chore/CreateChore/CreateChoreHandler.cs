using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.CreateChore;

public class CreateChoreHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<CreateChoreCommand>
{
    public async Task Handle(CreateChoreCommand request, CancellationToken cancellationToken)
    {
        var assignedKidIds = request.AssignedKids.Select(ak => ak.KidId).ToList();
        var resourceParentIds = await context.Kids
            .Where(k => assignedKidIds.Contains(k.KidId))
            .Select(k => k.ParentId)
            .ToListAsync(cancellationToken);

        AuthorisationHelper.EnsureAssignedKidIdsAreValid(resourceParentIds, assignedKidIds);

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsAllResources(resourceParentIds, parentId);

        var chore = ChoreE.Create
        (
            request.CategoryId,
            request.Name,
            request.Icon,
            request.Description,
            request.Points,
            request.Difficulty,
            request.Frequency
        );

        foreach (var assignedKid in request.AssignedKids)
            chore.KidChores.Add(KidChore.Create(assignedKid.KidId, assignedKid.DueDay, assignedKid.IsVisible));

        await context.Chores.AddAsync(chore, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}