using ChorePoint.Domain.Enums;

using MediatR;

namespace ChorePoint.Domain.Entities;

public class IntegrationEvent : EntityBase
{
    public int IntegrationEventId { get; set; }
    public int EntityId { get; set; }
    public IntegrationEventStatus Status { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public int RetryCount { get; set; }
}
