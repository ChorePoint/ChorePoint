using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChorePoint.Domain.Entities;

[Table("users")]
public class User
{
    [Key] [Column("id")] public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(10)]
    [Column("avatar")]
    public string Avatar { get; set; } = null!;

    [Column("age")] public int? Age { get; set; }

    [Required] [Column("day_streak")] public int DayStreak { get; set; }

    [Required] [Column("total_points")] public int TotalPoints { get; set; }

    [Required] [Column("points_today")] public int PointsToday { get; set; }

    [Column("created_at")] public DateTime? CreatedAt { get; set; }

    [Column("updated_at")] public DateTime? UpdatedAt { get; set; }

    [Column("parent_id")] public int ParentId { get; set; }

    // Navigation property
    public ICollection<Chore> Chores { get; set; } = [];
    public Parent Parent { get; set; } = null!;
}