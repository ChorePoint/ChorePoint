using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.Chore.UpdateChore;

public class UpdateChoreHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<UpdateChoreCommand>
{
    public async Task Handle(UpdateChoreCommand request, CancellationToken cancellationToken)
    {
        var chore = await context.Chores
            .Include(c => c.KidChores)
            .SingleOrDefaultAsync(c => c.ChoreId.Equals(request.ChoreId), cancellationToken);

        if (chore is null)
            throw new NotFoundException($"No chore exists with ID [{request.ChoreId}]");

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(chore.ParentId, parentId);

        chore.Update(request.CategoryId, request.Name, request.Icon, request.Description, request.Points,
            request.Difficulty, request.Frequency);

        foreach (var assignedKid in request.AssignedKids)
        {
            var kidChore = chore.KidChores.SingleOrDefault(kc => kc.KidId.Equals(assignedKid.KidId));

            if (kidChore is null)
                throw new DomainException(
                    $"Kid with ID [{assignedKid.KidId}] is not assigned to chore with ID [{request.ChoreId}]");

            kidChore.Update(assignedKid.DueDay, assignedKid.IsVisible);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}