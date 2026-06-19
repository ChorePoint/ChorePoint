using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ChoreE = ChorePoint.Domain.Entities.Chore;

namespace ChorePoint.Application.Handlers.Chore.UpdateChore;

public class UpdateChoreHandler(IAppDbContext context, IParentContextService parentContextService) : IRequestHandler<UpdateChoreCommand>
{
    public async Task Handle(UpdateChoreCommand request, CancellationToken cancellationToken)
    {
        var existingChore = await context.Chores
            .Include(c  => c.KidChores)
            .FirstOrDefaultAsync(c => c.ChoreId.Equals(request.ChoreId), cancellationToken);

        if (existingChore is null)
            throw new NotFoundException($"No chore exists with ID [{request.ChoreId}]");

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(existingChore.ParentId, parentId);

        existingChore.Update(request.Name, request.Icon, request.Description, request.Points, request.Difficulty, request.Frequency);

        foreach (var assignedKid in request.AssignedKids)
        {
            var existingKidChore = existingChore.KidChores.FirstOrDefault(kc => kc.KidId.Equals(assignedKid.KidId));
            
            if (existingKidChore is null)
                throw new NotFoundException($"No assigned kid with ID [{assignedKid.KidId}] exists on chore with ID [{request.ChoreId}]");
            
            existingKidChore.Update(assignedKid.DueDay, assignedKid.IsVisible);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}