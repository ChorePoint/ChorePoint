namespace ChorePoint.Domain.IntegrationEventModels;

public class CompletionStreakIncreasedIntegrationEvent(int kidId, int choreId, int completionStreak) : IntegrationEventModelBase
{
    public int KidId { get; } = kidId;
    public int ChoreId { get; } = choreId;
    public int CompletionStreak { get; } = completionStreak;
}
