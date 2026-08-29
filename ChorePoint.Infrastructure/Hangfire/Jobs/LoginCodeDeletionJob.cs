using ChorePoint.Application.Interfaces.Hangfire;

using Microsoft.Extensions.Logging;

namespace ChorePoint.Infrastructure.Hangfire.Jobs;

public partial class LoginCodeDeletionJob(AppDbContext context, ILogger<LoginCodeDeletionJob> logger) : ILoginCodeDeletionJob
{
    public async Task StartDeleteJob(int kidId, CancellationToken cancellationToken)
    {
        var loginCode = await context.LoginCodes.FindAsync([kidId], cancellationToken);

        if (loginCode is null)
        {
            LogLoginCodeNotFound(kidId);
            return;
        }

        context.LoginCodes.Remove(loginCode);
        await context.SaveChangesAsync(cancellationToken);
    }

    [LoggerMessage(LogLevel.Error, "No login code found for kid with ID [{KidId}] during deletion job")]
    partial void LogLoginCodeNotFound(int kidId);
}
