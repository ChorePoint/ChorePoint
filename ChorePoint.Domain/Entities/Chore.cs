using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;

namespace ChorePoint.Domain.Entities;

[Table("chores")]
public class Chore : EntityBase
{
    [Key] [Column("chore_id")] public int ChoreId { get; set; }

    [Column("parent_id")] public int ParentId { get; set; }

    [Column("kid_id")] public int KidId { get; set; }

    [MaxLength(150)] [Column("name")] public string Name { get; set; }

    [MaxLength(10)] [Column("icon")] public string Icon { get; set; }

    [MaxLength(300)]
    [Column("description")]
    public string? Description { get; set; }

    [Column("points")] public int Points { get; set; }

    [MaxLength(10)] [Column("difficulty")] public ChoreDifficulty Difficulty { get; set; }

    [MaxLength(10)] [Column("frequency")] public ChoreFrequency Frequency { get; set; }

    [Column("due_day")] public DayOfWeek? DueDay { get; set; }

    [Column("last_completed_at")] public DateTime? LastCompletedAt { get; set; }

    [Column("completion_count")] public int CompletionCount { get; set; }

    [Column("is_visible")] public bool IsVisible { get; set; }


    [ForeignKey(nameof(ParentId))] public Parent Parent { get; set; }

    [ForeignKey(nameof(KidId))] public Kid Kid { get; set; }


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
            ChoreId = ChoreId,
            KidId = KidId,
            CompletedAt = now,
            ApprovalStatus = ChoreApprovalStatus.Approved,
            ApprovedAt = now
        };
    }

    public void Update(int kidId, string name, string icon, int points, ChoreDifficulty difficulty,
        ChoreFrequency frequency, bool isVisible, string? description, DayOfWeek? dueDay)
    {
        KidId = kidId;
        Name = name;
        Icon = icon;
        Points = points;
        Difficulty = difficulty;
        Frequency = frequency;
        IsVisible = isVisible;
        Description = description;
        DueDay = dueDay;
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