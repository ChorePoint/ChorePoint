using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.DomainEventModels;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.IntegrationEventModels;

using MediatR;

namespace ChorePoint.Application.DomainEventHandlers.CompletionStreakBroken;

public class OnCompletionStreakBrokenDomainEventWriteIntegrationEventHandler(IAppDbContext context) : INotificationHandler<CompletionStreakBrokenDomainEvent>
{
    public async Task Handle(CompletionStreakBrokenDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var kidChore = domainEvent.KidChore;
        var integrationEventModel = new CompletionStreakBrokenIntegrationEvent(kidChore.KidId, kidChore.ChoreId, kidChore.CompletionStreak);

        var integrationEventEntity = IntegrationEvent.Create("KidChore", integrationEventModel);
        await context.IntegrationEvents.AddAsync(integrationEventEntity, cancellationToken);
    }
}
