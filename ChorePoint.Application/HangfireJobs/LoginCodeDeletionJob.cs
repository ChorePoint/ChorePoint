using ChorePoint.Application.Interfaces;

using Microsoft.Extensions.Logging;

namespace ChorePoint.Application.HangfireJobs;

internal interface ILoginCodeDeletionJob
{
    Task StartLoginCodeDeletionJob(int kidId, CancellationToken cancellationToken);
}

internal sealed partial class LoginCodeDeletionJob(IAppDbContext context, ILogger<LoginCodeDeletionJob> logger) : ILoginCodeDeletionJob
{
    public async Task StartLoginCodeDeletionJob(int kidId, CancellationToken cancellationToken)
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

    [LoggerMessage(LogLevel.Information, "No login code found for kid with ID [{KidId}] during login code deletion job")]
    partial void LogLoginCodeNotFound(int kidId);
}
