using ChorePoint.Domain.Entities;

using MediatR;

namespace ChorePoint.Domain.DomainEventModels;

public class CompletionStreakBrokenDomainEvent(KidChore kidChore) : INotification
{
    public KidChore KidChore { get; } = kidChore;
}
