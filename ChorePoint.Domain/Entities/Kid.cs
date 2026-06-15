using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChorePoint.Domain.Exceptions;

namespace ChorePoint.Domain.Entities;

public class Kid : EntityBase
{
    public int KidId { get; set; }
    public int ParentId { get; set; }
    public int KidChoreId { get; set; }
    public int KidShopItemId { get; set; }
    
    public string Name { get; set; }
    public string Avatar { get; set; }
    public int? Age { get; set; }
    public int DayStreak { get; set; }
    public int LifetimePoints { get; set; }
    public int SpendablePoints { get; set; }
    
    public Parent Parent { get; set; }
    public ICollection<Chore> Chores { get; set; }
    public ICollection<KidChore> KidChores { get; set; }
    public ICollection<ShopItem> ShopItems { get; set; }
    public ICollection<KidShopItem> KidShopItems { get; set; }


    public void SpendPoints(int pointsToSubtract)
    {
        if (pointsToSubtract > SpendablePoints)
            throw new DomainException($"Kid with ID [{KidId}] does not have enough spendable points!");

        SpendablePoints -= pointsToSubtract;
    }
}