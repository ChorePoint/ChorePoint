using ChorePoint.Domain.Entities;

using MediatR;

namespace ChorePoint.Domain.DomainEventModels;

public class LoginCodeCreatedDomainEvent(LoginCode loginCode) : INotification
{
    public LoginCode LoginCode { get; } = loginCode;
}
