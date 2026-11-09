using MediatR;

namespace ChorePoint.Domain.Entities;

public class EntityBase
{
    private List<INotification>? _domainEvents;
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly() ?? [];

    public void AddDomainEvent(INotification domainEvent)
    {
        _domainEvents ??= [];
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(INotification domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
