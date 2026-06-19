namespace ChorePoint.Domain.Entities;

public class Category
{
    public int CategoryId { get; set; }
    
    public string Name { get; set; }
    public string Icon { get; set; }
    
    public ICollection<ShopItem> ShopItems { get; set; }
}