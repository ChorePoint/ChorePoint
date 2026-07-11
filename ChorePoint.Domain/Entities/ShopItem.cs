namespace ChorePoint.Domain.Entities;

public class ShopItem : EntityBase
{
    public int ShopItemId { get; set; }
    public int ParentId { get; set; }
    public int? CategoryId { get; set; }

    public string Name { get; set; }
    public string Icon { get; set; }
    public string? Description { get; set; }
    public int Cost { get; set; }
    public int? Quantity { get; set; }

    public Parent Parent { get; set; }
    public Category? Category { get; set; }
    public ICollection<Kid> Kids { get; set; } = new List<Kid>();
    public ICollection<KidShopItem> KidShopItems { get; set; } = new List<KidShopItem>();

    public static ShopItem Create(
        int parentId,
        int? categoryId,
        string name,
        string icon,
        string? description,
        int cost,
        int? quantity
    )
    {
        return new ShopItem
        {
            ParentId = parentId,
            CategoryId = categoryId,
            Name = name,
            Icon = icon,
            Description = description,
            Cost = cost,
            Quantity = quantity,
        };
    }

    public void Update(
        int? categoryId,
        string name,
        string icon,
        string? description,
        int cost,
        int? quantity
    )
    {
        CategoryId = categoryId;
        Name = name;
        Icon = icon;
        Description = description;
        Cost = cost;
        Quantity = quantity;
    }
}
