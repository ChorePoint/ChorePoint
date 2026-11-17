using ChorePoint.Domain.Entities;

using MediatR;

namespace ChorePoint.Domain.DomainEventModels;

public class CompletionStreakIncreasedDomainEvent(KidChore kidChore) : INotification
{
    public KidChore KidChore { get; set; } = kidChore;
}
