using ChorePoint.Application.Interfaces;
using ChorePoint.Domain.DomainEventModels;
using ChorePoint.Domain.Entities;
using ChorePoint.Domain.IntegrationEventModels;

using MediatR;

namespace ChorePoint.Application.DomainEventHandlers.LoginCodeCreated;

public class OnLoginCodeCreatedDomainEventWriteIntegrationEventHandler(IAppDbContext context) : INotificationHandler<LoginCodeCreatedDomainEvent>
{
    public async Task Handle(LoginCodeCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var loginCode = domainEvent.LoginCode;
        var integrationEventModel = new LoginCodeCreatedIntegrationEvent(loginCode.KidId, loginCode.Code);

        var integrationEventEntity = IntegrationEvent.Create("LoginCode", integrationEventModel);
        await context.IntegrationEvents.AddAsync(integrationEventEntity, cancellationToken);
    }
}
