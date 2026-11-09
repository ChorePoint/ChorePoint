using MediatR;

namespace ChorePoint.Domain.DomainEventModels;

public class CompletionStreakIncreasedDomainEvent(int kidChore, int choreId, int completionStreak) : INotification
{
    public int KidChore { get; } = kidChore;
    public int ChoreId { get; } = choreId;
    public int CompletionStreak { get; } = completionStreak;
}
