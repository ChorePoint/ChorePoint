using ChorePoint.Application.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChorePoint.Application.HangfireJobs;

internal interface ICompletionStreakResetJob
{
    Task StartCompletionStreakResetJob(int kidId, int choreId, CancellationToken cancellationToken);
}

internal sealed partial class CompletionStreakResetJob(IAppDbContext context, ILogger<CompletionStreakResetJob> logger) : ICompletionStreakResetJob
{
    public async Task StartCompletionStreakResetJob(int kidId, int choreId, CancellationToken cancellationToken)
    {
        var kid = await context.Kids
            .Include(k => k.KidChores)
            .SingleOrDefaultAsync(k => k.KidId.Equals(kidId), cancellationToken);

        if (kid is null)
        {
            LogKidNotFound(kidId);
            return;
        }

        var kidChore = kid.KidChores.SingleOrDefault(kc => kc.ChoreId.Equals(choreId));

        if (kidChore is null)
        {
            LogKidChoreNotFound(choreId, kidId);
            return;
        }

        kidChore.ResetCompletionStreak();

        await context.SaveChangesAsync(cancellationToken);
    }

    [LoggerMessage(LogLevel.Information, "No kid found with ID [{KidId}] during completion streak reset job")]
    partial void LogKidNotFound(int kidId);

    [LoggerMessage(LogLevel.Information, "No chore with ID [{ChoreId}] found assigned to kid with ID [{KidId}] during completion streak reset job")]
    partial void LogKidChoreNotFound(int choreId, int kidId);
}
