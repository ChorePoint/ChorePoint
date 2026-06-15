using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChorePoint.Domain.Enums;

namespace ChorePoint.Domain.Entities;

public class KidShopItem : EntityBase
{
    public int KidId { get; set; }
    public int ShopItemId { get; set; }
    
    public ShopItemStatus Status { get; set; }
}