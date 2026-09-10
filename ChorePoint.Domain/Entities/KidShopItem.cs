using ChorePoint.Domain.Exceptions;

namespace ChorePoint.Domain.Entities;

public class KidShopItem : EntityBase
{
    public int KidId { get; set; }
    public int ShopItemId { get; set; }

    public bool PendingApproval { get; set; }
    public bool IsVisible { get; set; }

    public static KidShopItem Create(int kidId, bool isVisible, bool pendingApproval = false)
    {
        return new KidShopItem
        {
            KidId = kidId,
            PendingApproval = pendingApproval,
            IsVisible = isVisible
        };
    }

    public void Buy(Kid kid, ShopItem shopItem, bool purchaseRequiresApproval)
    {
        if (shopItem.Quantity is not null && shopItem.Quantity.Equals(0))
        {
            throw new DomainException($"Shop item with ID [{ShopItemId}] is out of stock");
        }

        if (PendingApproval)
        {
            throw new DomainException(
                $"Kid with ID [{KidId}] attempted to purchase shop item with ID [{ShopItemId}] that is already pending approval");
        }

        if (purchaseRequiresApproval)
        {
            PendingApproval = true;
        }
        else
        {
            kid.SpendPoints(shopItem.Cost);

            if (shopItem.Quantity is null)
            {
                return;
            }

            shopItem.Quantity -= 1;
        }
    }

    public void ResetApprovalStatus()
    {
        PendingApproval = false;
    }
}
