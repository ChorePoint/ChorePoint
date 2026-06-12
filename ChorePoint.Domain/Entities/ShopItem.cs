using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChorePoint.Domain.Enums;

namespace ChorePoint.Domain.Entities;

[Table("shop_items")]
public class ShopItem : EntityBase
{
    [Key] [Column("shop_item_id")] public int ShopItemId { get; set; }

    [Column("parent_id")] public int ParentId { get; set; }

    [Column("kid_id")] public int KidId { get; set; }

    [MaxLength(50)] [Column("name")] public string Name { get; set; }

    [Column("cost")] public int Cost { get; set; }

    [MaxLength(10)] [Column("status")] public ShopItemStatus Status { get; set; }

    [Column("quantity")] public int Quantity { get; set; }


    [ForeignKey(nameof(ParentId))] public Parent Parent { get; set; }

    [ForeignKey(nameof(KidId))] public Kid Kid { get; set; }


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