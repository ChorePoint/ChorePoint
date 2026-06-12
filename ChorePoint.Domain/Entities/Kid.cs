using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChorePoint.Domain.Exceptions;

namespace ChorePoint.Domain.Entities;

[Table("kids")]
public class Kid : EntityBase
{
    [Key] [Column("kid_id")] public int KidId { get; set; }

    [Column("parent_id")] public int ParentId { get; set; }

    [MaxLength(100)] [Column("name")] public string Name { get; set; }

    [MaxLength(10)] [Column("avatar")] public string Avatar { get; set; }

    [Column("age")] public int? Age { get; set; }

    [Column("day_streak")] public int DayStreak { get; set; }

    [Column("total_points")] public int TotalPoints { get; set; }

    [Column("points_today")] public int PointsToday { get; set; }

    [Column("spendable_points")] public int SpendablePoints { get; set; }


    [ForeignKey(nameof(ParentId))] public Parent Parent { get; set; }


    public void SpendPoints(int pointsToSubtract)
    {
        if (pointsToSubtract > SpendablePoints)
            throw new DomainException($"Kid with ID [{KidId}] does not have enough spendable points!");

        SpendablePoints -= pointsToSubtract;
    }
}