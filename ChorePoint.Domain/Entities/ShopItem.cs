using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChorePoint.Domain.Enums;

namespace ChorePoint.Domain.Entities;

[Table("shop_items")]
public class ShopItem
{
    [Key] [Column("id")] public int Id { get; set; }

    [Required] [Column("parent_id")] public int ParentId { get; set; }
    
    [Required]
    [Column("kid_id")]
    public int KidId { get; set; }
    
    [Required]
    [MaxLength(50)]
    [Column("name")]
    public string Name { get; set; }
    
    [Required]
    [Column("cost")]
    public int Cost { get; set; }
    
    [Required]
    [Column("status")]
    public ShopItemStatus Status { get; set; }
    
    [Column("created_at")] public DateTime? CreatedAt { get; set; }

    [Column("updated_at")] public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey(nameof(ParentId))] public Parent Parent { get; set; } = null!;
    
    [ForeignKey(nameof(KidId))] public Kid Kid { get; set; } = null!;
    
    
    public static ShopItem Create(int parentId, int kidId, string name, int cost, DateTime now)
    {
        return new ShopItem
        {
            ParentId = parentId,
            KidId = kidId,
            Name = name,
            Cost = cost,
            Status = ShopItemStatus.Available,
            CreatedAt = now
        };
    }
}