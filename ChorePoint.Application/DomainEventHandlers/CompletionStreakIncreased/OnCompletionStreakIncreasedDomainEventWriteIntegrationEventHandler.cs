using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.DomainEventModels;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.IntegrationEventModels;

using MediatR;

namespace ChorePoint.Application.DomainEventHandlers.CompletionStreakIncreased;

public class OnCompletionStreakIncreasedDomainEventWriteIntegrationEventHandler(IAppDbContext context) : INotificationHandler<CompletionStreakIncreasedDomainEvent>
{
    public async Task Handle(CompletionStreakIncreasedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var kidChore = domainEvent.KidChore;
        var integrationEventModel = new CompletionStreakIncreasedIntegrationEvent(kidChore.KidId, kidChore.ChoreId, kidChore.CompletionStreak);

        var integrationEventEntity = IntegrationEvent.Create("KidChore", integrationEventModel);
        await context.IntegrationEvents.AddAsync(integrationEventEntity, cancellationToken);
    }
}
