using ChorePoint.Application.Authorisation;
using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;

using MediatR;

using Microsoft.EntityFrameworkCore;

using ChoreSubmissionE = ChorePoint.Domain.Entities.ChoreSubmission;

namespace ChorePoint.Application.Handlers.ChoreSubmission.CompleteChore;

public class CompleteChoreHandler(IAppDbContext context, IParentContextService parentContextService) : IRequestHandler<CompleteChoreCommand>
{
    public async Task Handle(CompleteChoreCommand request, CancellationToken cancellationToken)
    {
        var chore = await context.Chores.FindAsync([request.ChoreId], cancellationToken);

        if (chore is null)
        {
            throw new NotFoundException($"No chore exists with ID [{request.ChoreId}]");
        }

        var parentId = parentContextService.GetParentId();
        AuthorisationHelper.EnsureParentOwnsResource(chore.ParentId, parentId);

        var latestSubmission = await context.ChoreSubmissions
            .Where(cs => cs.ChoreId.Equals(request.ChoreId))
            .Where(cs => cs.KidId.Equals(request.KidId))
            .OrderByDescending(cs => cs.CompletedAt)
            .FirstOrDefaultAsync(cancellationToken);

        var now = DateTime.UtcNow;
        if (latestSubmission is not null)
        {
            chore.EnsureCanBeCompleted(latestSubmission, now);
        }

        var newSubmission = chore.CreateSubmission(request.KidId, now);
        await context.ChoreSubmissions.AddAsync(newSubmission, cancellationToken);

        var autoApproveChore = await context.ParentSettings
            .Where(ps => ps.ParentId.Equals(parentId))
            .Select(ps => ps.AutoApproveChores)
            .SingleOrDefaultAsync(cancellationToken);

        if (autoApproveChore)
        {
            // Explicitly load reference navigation properties as they are not loaded on a newly created entity
            await context.Entry(newSubmission).Reference(nameof(ChoreSubmissionE.Chore)).LoadAsync(cancellationToken);
            await context.Entry(newSubmission).Reference(nameof(ChoreSubmissionE.Kid)).LoadAsync(cancellationToken);

            newSubmission.Review("Auto-approved", true, now);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
