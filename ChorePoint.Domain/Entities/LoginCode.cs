namespace ChorePoint.Domain.Entities;

public class LoginCode : EntityBase
{
    public int KidId { get; set;}

    public string Code { get; set; } = string.Empty;

    public Kid Kid { get; set; } = null!;

    public static LoginCode Create(int kidId, string code)
    {
        return new LoginCode
        {
            KidId = kidId,
            Code = code
        };
    }
}
