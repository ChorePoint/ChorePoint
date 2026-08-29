namespace ChorePoint.Application.Interfaces.Hangfire;

public interface ILoginCodeDeletionJob
{
    Task StartDeleteJob(int kidId, CancellationToken cancellationToken);
}
