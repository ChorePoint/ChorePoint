using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.CompleteChore;

public class CompleteChoreHandler(IAppDbContext context, IParentContextService parentContextService)
    : IRequestHandler<CompleteChoreCommand>
{
    public async Task Handle(CompleteChoreCommand request, CancellationToken cancellationToken)
    {
        var chore = await context.Chores.FindAsync([request.ChoreId], cancellationToken);

        if (chore is null)
            throw new NotFoundException($"No chore exists with ID [{request.ChoreId}]");

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(chore.ParentId, parentId);

        var latestSubmission = await context
            .ChoreSubmissions.Where(cs => cs.ChoreId.Equals(request.ChoreId))
            .Where(cs => cs.KidId.Equals(request.KidId))
            .OrderByDescending(cs => cs.CompletedAt)
            .FirstOrDefaultAsync(cancellationToken);

        var now = DateTime.UtcNow;
        if (latestSubmission is not null)
            chore.EnsureCanBeCompleted(latestSubmission, now);

        var newSubmission = chore.CreateSubmission(request.KidId, now);

        await context.ChoreSubmissions.AddAsync(newSubmission, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
