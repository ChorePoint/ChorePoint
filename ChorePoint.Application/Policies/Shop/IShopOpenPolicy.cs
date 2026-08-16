namespace ChorePoint.Application.Policies.Shop;

public interface IShopOpenPolicy
{
    Task EnsureShopIsOpen(CancellationToken cancellationToken);
}
