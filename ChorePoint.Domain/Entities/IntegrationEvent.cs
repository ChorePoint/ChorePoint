using System.Text.Json;

using ChorePoint.Domain.Enums;
using ChorePoint.Domain.IntegrationEventModels;

namespace ChorePoint.Domain.Entities;

public class IntegrationEvent : EntityBase
{
    public int IntegrationEventId { get; set; }
    public IntegrationEventStatus Status { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public int RetryCount { get; set; }

    public static IntegrationEvent Create(string entityType, IntegrationEventModelBase integrationEvent)
    {
        var eventType = integrationEvent.GetType();
        return new IntegrationEvent
        {
            Status = IntegrationEventStatus.Waiting,
            EntityType = entityType,
            EventType = eventType.Name
                        ?? throw new InvalidOperationException($"Event type [{eventType}] used in IntegrationEvent.Create(...) has a null Name property"),
            Payload = JsonSerializer.Serialize(integrationEvent, eventType),
            RetryCount = 0
        };
    }
}
