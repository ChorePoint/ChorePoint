using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Exceptions;

namespace ChorePoint.Domain.Services;

public static class ShopItemPurchaseService
{
    public static void Purchase(bool purchaseRequiresApproval, KidShopItem kidShopItem, ShopItem shopItem, Kid kid)
    {
        if (purchaseRequiresApproval)
        {
            kidShopItem.SetToPendingApproval();
        }
        else
        {
            shopItem.Buy();
            kid.SpendPoints(shopItem.Cost);
        }
    }

    public static void ReviewPurchase(bool approvePurchase, KidShopItem kidShopItem, ShopItem shopItem, Kid kid)
    {
        if (!kidShopItem.PendingApproval)
        {
            throw new DomainException(
                $"Shop item with ID [{shopItem.ShopItemId}] needs to be pending approval for kid with ID [{kidShopItem.KidId}]"
            );
        }

        kidShopItem.ResetPendingApproval();

        if (!approvePurchase)
        {
            return;
        }

        shopItem.Buy();
        kid.SpendPoints(shopItem.Cost);
    }
}
