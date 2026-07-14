using ChorePoint.Domain.Enums;

namespace ChorePoint.Domain.Entities;

public class KidShopItem : EntityBase
{
    public int KidId { get; set; }
    public int ShopItemId { get; set; }

    public ShopItemStatus Status { get; set; }

    public static KidShopItem Create(int kidId, ShopItemStatus status = ShopItemStatus.Available)
    {
        return new KidShopItem { KidId = kidId, Status = status };
    }

    public void Update(ShopItemStatus status)
    {
        Status = status;
    }

    public void Buy(
        ShopItem shopItem,
        bool purchaseRequiresApproval,
        IReadOnlyList<KidShopItem> otherKidShopItems
    )
    {
        if (purchaseRequiresApproval)
        {
            Status = ShopItemStatus.Pending;
        }
        else
        {
            if (shopItem.Quantity is null)
                return;

            shopItem.Quantity -= 1;

            if (shopItem.Quantity != 0)
                return;

            Status = ShopItemStatus.Hidden;
            foreach (var kidShopItem in otherKidShopItems)
                kidShopItem.Status = ShopItemStatus.Hidden;
        }
    }

    public void Reactivate(ShopItem shopItem, int? quantity)
    {
        Status = ShopItemStatus.Available;
        shopItem.Quantity = quantity;
    }

    public void ResetStatus()
    {
        Status = ShopItemStatus.Available;
    }
}
