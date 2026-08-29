namespace ChorePoint.Application.Policies.Shop;

public interface IShopOpenPolicy
{
    Task EnsureShopIsOpenIfKid(CancellationToken cancellationToken);
}
