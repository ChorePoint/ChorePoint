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

    public void SetToPendingApproval()
    {
        if (PendingApproval)
        {
            throw new DomainException(
                $"Kid with ID [{KidId}] attempted to purchase shop item with ID [{ShopItemId}] that is already pending approval");
        }

        PendingApproval = true;
    }

    public void ResetPendingApproval()
    {
        PendingApproval = false;
    }
}
