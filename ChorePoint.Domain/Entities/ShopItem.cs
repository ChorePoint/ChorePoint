using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChorePoint.Domain.Enums;

namespace ChorePoint.Domain.Entities;

public class ShopItem : EntityBase
{
    public int ShopItemId { get; set; }
    public int ParentId { get; set; }
    public int KidShopItemId { get; set; }
    
    public string Name { get; set; }
    public int Cost { get; set; }
    public int Quantity { get; set; }
    
    public Parent Parent { get; set; }
    public ICollection<Kid> Kids { get; set; }
    public ICollection<KidShopItem> KidShopItems { get; set; }


    public static ShopItem Create(int parentId, int kidId, string name, int cost, int quantity, DateTime now)
    {
        return new ShopItem
        {
            ParentId = parentId,
            KidId = kidId,
            Name = name,
            Cost = cost,
            Status = ShopItemStatus.Available,
            Quantity = quantity,
            CreatedAt = now
        };
    }

    public void Update(string name, int cost, ShopItemStatus status, int quantity)
    {
        Name = name;
        Cost = cost;
        Status = status;
        Quantity = quantity;
    }

    public void Buy(bool purchaseRequiresApproval)
    {
        if (purchaseRequiresApproval)
        {
            Status = ShopItemStatus.Pending;
        }
        else
        {
            Quantity = -1;
            if (Quantity == 0)
                Status = ShopItemStatus.Hidden;
        }
    }

    public void Reactivate(int quantity)
    {
        Status = ShopItemStatus.Available;
        Quantity = quantity;
    }

    public void ResetStatus()
    {
        Status = ShopItemStatus.Available;
    }
}