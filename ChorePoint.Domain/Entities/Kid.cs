using ChorePoint.Domain.Exceptions;

namespace ChorePoint.Domain.Entities;

public class Kid : EntityBase
{
    public int KidId { get; set; }
    public int ParentId { get; set; }

    public string Name { get; set; }
    public string Avatar { get; set; }
    public int? Age { get; set; }
    public int DayStreak { get; set; }
    public int LifetimePoints { get; set; }
    public int SpendablePoints { get; set; }
    public int? LoginCode { get; set; }

    public Parent Parent { get; set; }
    public ICollection<Chore> Chores { get; set; } = new List<Chore>();
    public ICollection<KidChore> KidChores { get; set; } = new List<KidChore>();
    public ICollection<ShopItem> ShopItems { get; set; } = new List<ShopItem>();
    public ICollection<KidShopItem> KidShopItems { get; set; } = new List<KidShopItem>();

    public static Kid Create(int parentId, string name, string avatar, int? age)
    {
        return new Kid
        {
            ParentId = parentId,
            Name = name,
            Avatar = avatar,
            Age = age,
        };
    }

    public void Update(
        string name,
        string avatar,
        int? age,
        int dayStreak,
        int lifetimePoints,
        int spendablePoints
    )
    {
        Name = name;
        Avatar = avatar;
        Age = age;
        DayStreak = dayStreak;
        LifetimePoints = lifetimePoints;
        SpendablePoints = spendablePoints;
    }

    public void SpendPoints(int pointsToSubtract)
    {
        if (pointsToSubtract > SpendablePoints)
            throw new DomainException(
                $"Kid with ID [{KidId}] does not have enough spendable points!"
            );

        SpendablePoints -= pointsToSubtract;
    }
}
