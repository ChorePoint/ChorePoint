namespace ChorePoint.Application.HangfireJobs;

public interface ILoginCodeDeletionJob
{
    Task StartDeleteJob(int kidId, CancellationToken cancellationToken);
}
