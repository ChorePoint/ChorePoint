using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;

namespace ChorePoint.Domain.Entities;

[Table("chores")]
public sealed class Chore
{
    [Key] [Column("id")] public int Id { get; set; }

    [Required] [Column("user_id")] public int KidId { get; set; }

    [Required]
    [MaxLength(150)]
    [Column("name")]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(10)]
    [Column("icon")]
    public string Icon { get; set; } = "🎯";

    [Required] [Column("points")] public int Points { get; set; }

    [Required] [Column("difficulty")] public ChoreDifficulty Difficulty { get; set; }

    [Required] [Column("frequency")] public ChoreFrequency Frequency { get; set; } = ChoreFrequency.Daily;

    [Column("due_day")] // Only set when frequency is weekly. Determines which day of the week the chore cycles on.
    public DayOfWeek? DueDay { get; set; } = DayOfWeek.Monday;

    [Required] [Column("is_visible")] public bool IsVisible { get; set; } = true;

    [Column("last_completed_at")] public DateTime? LastCompletedAt { get; set; }

    [Column("created_at")] public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")] public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    [Column("completion_count")] public int CompletionCount { get; set; }

    [Column("description")] public string? Description { get; set; }

    // Navigation property
    public Kid Kid { get; set; } = null!;


    public static Chore Create(string name, string icon, int points, ChoreDifficulty difficulty,
        ChoreFrequency frequency, DayOfWeek? dueDay, int kidId, string description, DateTime now)
    {
        return new Chore
        {
            Name = name,
            Icon = icon,
            Points = points,
            Difficulty = difficulty,
            Frequency = frequency,
            DueDay = dueDay,
            KidId = kidId,
            Description = description,
            CreatedAt = now
        };
    }

    public ChoreSubmission CreateSubmission(DateTime now)
    {
        return new ChoreSubmission
        {
            ChoreId = Id,
            KidId = KidId,
            CompletedAt = now,
            ApprovalStatus = ChoreApprovalStatus.Approved,
            ApprovedAt = now
        };
    }

    public void EnsureCanBeCompleted(ChoreSubmission? currentSubmission, DateTime now)
    {
        switch (Frequency)
        {
            case ChoreFrequency.Daily:
                if (currentSubmission?.CompletedAt.Date == now.Date)
                    throw new ChoreAlreadyCompletedException("Chore already completed today");
                break;
            case ChoreFrequency.Weekly:
                if (currentSubmission?.CompletedAt.AddDays(7) > now)
                    throw new ChoreAlreadyCompletedException("Chore already completed within the last week");
                break;
            case ChoreFrequency.Bonus:
            default:
                throw new DomainException($"Unexpected frequency: {Frequency}");
        }
    }
}