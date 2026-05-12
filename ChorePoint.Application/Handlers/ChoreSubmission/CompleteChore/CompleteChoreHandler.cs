using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChorePoint.Application.Handlers.ChoreSubmission.CompleteChore;

public class CompleteChoreHandler(IAppDbContext context) : IRequestHandler<CompleteChoreCommand>
{
    public async Task Handle(CompleteChoreCommand request, CancellationToken cancellationToken)
    {
        var chore = await context.Chores
            .FindAsync([request.Id], cancellationToken);

        var lastCompletion = await context.ChoreSubmissions
            .Where(c => c.ChoreId == request.Id)
            .OrderByDescending(c => c.CompletedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (chore == null) throw new NotFoundException($"No chores exist with id: {request.Id}");

        var now = DateTime.UtcNow;
        chore.EnsureCanBeCompleted(lastCompletion, now);
        var completion = chore.CreateSubmission(now);

        await context.ChoreSubmissions.AddAsync(completion, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}