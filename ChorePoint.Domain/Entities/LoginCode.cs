using ChorePoint.Domain.DomainEventModels;

namespace ChorePoint.Domain.Entities;

public class LoginCode : EntityBase
{
    public int KidId { get; set; }

    public string Code { get; set; } = string.Empty;

    public Kid Kid { get; set; } = null!;

    public static LoginCode Create(int kidId, string code)
    {
        var loginCode = new LoginCode
        {
            KidId = kidId,
            Code = code
        };
        loginCode.AddDomainEvent(new LoginCodeCreatedDomainEvent(loginCode));

        return loginCode;
    }

}
