using ChorePoint.Application.HangfireJobs;
using ChorePoint.Domain.DomainEventModels;

using Hangfire;

using MediatR;

using Microsoft.Extensions.Options;

namespace ChorePoint.Application.DomainEventHandlers.LoginCodeCreated;

public class OnLoginCodeCreatedDomainEventScheduleDeletionJobHandler(IBackgroundJobClient backgroundJobClient, IOptionsSnapshot<ApiOptions> apiOptions)
    : INotificationHandler<LoginCodeCreatedDomainEvent>
{
    private readonly ApiOptions _apiOptions = apiOptions.Value;

    public Task Handle(LoginCodeCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var loginCode = domainEvent.LoginCode;
        backgroundJobClient.Schedule<ILoginCodeDeletionJob>(
            j => j.StartLoginCodeDeletionJob(loginCode.KidId, cancellationToken),
            TimeSpan.FromMinutes(_apiOptions.KidLoginCodeTimeout)
        );

        return Task.CompletedTask;
    }
}
