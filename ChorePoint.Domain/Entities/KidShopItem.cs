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

    public void Update(bool pendingApproval, bool isVisible)
    {
        PendingApproval = pendingApproval;
        IsVisible = isVisible;
    }

    public void Buy(ShopItem shopItem, bool purchaseRequiresApproval)
    {
        if (purchaseRequiresApproval)
        {
            PendingApproval = true;
        }
        else
        {
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
