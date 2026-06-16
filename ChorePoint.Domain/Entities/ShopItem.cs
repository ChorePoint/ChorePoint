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


    public static ShopItem Create(int parentId, string name, int cost, int quantity)
    {
        return new ShopItem
        {
            ParentId = parentId,
            Name = name,
            Cost = cost,
            Quantity = quantity
        };
    }

    public void Update(string name, int cost, int quantity)
    {
        Name = name;
        Cost = cost;
        Quantity = quantity;
    }
}