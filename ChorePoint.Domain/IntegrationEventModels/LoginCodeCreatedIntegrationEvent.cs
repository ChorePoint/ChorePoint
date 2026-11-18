namespace ChorePoint.Domain.IntegrationEventModels;

public class LoginCodeCreatedIntegrationEvent(int kidId, string code) : IntegrationEventModelBase
{
    public int KidId { get; } = kidId;
    public string Code { get; } = code;
}
