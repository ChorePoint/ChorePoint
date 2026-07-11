using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Exceptions;

namespace ChorePoint.Domain.Entities;

public class Chore : EntityBase
{
    public int ChoreId { get; set; }
    public int ParentId { get; set; }
    public int? CategoryId { get; set; }

    public string Name { get; set; }
    public string Icon { get; set; }
    public string? Description { get; set; }
    public int Points { get; set; }
    public ChoreDifficulty Difficulty { get; set; }
    public ChoreFrequency Frequency { get; set; }
    public DateTime? LastCompletedAt { get; set; }
    public int CompletionCount { get; set; }

    public Parent Parent { get; set; }
    public Category? Category { get; set; }
    public ICollection<Kid> Kids { get; set; } = new List<Kid>();
    public ICollection<KidChore> KidChores { get; set; } = new List<KidChore>();

    public static Chore Create(
        int parentId,
        int? categoryId,
        string name,
        string icon,
        string description,
        int points,
        ChoreDifficulty difficulty,
        ChoreFrequency frequency
    )
    {
        return new Chore
        {
            ParentId = parentId,
            CategoryId = categoryId,
            Name = name,
            Icon = icon,
            Description = description,
            Points = points,
            Difficulty = difficulty,
            Frequency = frequency,
            CompletionCount = 0,
        };
    }

    public ChoreSubmission CreateSubmission(int kidId, DateTime now)
    {
        return new ChoreSubmission
        {
            ChoreId = ChoreId,
            ParentId = ParentId,
            KidId = kidId,
            ApprovalStatus = ChoreApprovalStatus.Pending,
            CompletedAt = now,
        };
    }

    public void Update(
        int? categoryId,
        string name,
        string icon,
        string? description,
        int points,
        ChoreDifficulty difficulty,
        ChoreFrequency frequency
    )
    {
        CategoryId = categoryId;
        Name = name;
        Icon = icon;
        Description = description;
        Points = points;
        Difficulty = difficulty;
        Frequency = frequency;
    }

    public void EnsureCanBeCompleted(ChoreSubmission latestSubmission, DateTime now)
    {
        switch (Frequency)
        {
            case ChoreFrequency.Daily:
                if (latestSubmission.CompletedAt.Date == now.Date)
                    throw new ChoreAlreadyCompletedException("Chore already completed today");
                break;
            case ChoreFrequency.Weekly:
                if (latestSubmission.CompletedAt.AddDays(7) > now)
                    throw new ChoreAlreadyCompletedException(
                        "Chore already completed within the last week"
                    );
                break;
            case ChoreFrequency.Bonus:
            default:
                throw new DomainException($"Unexpected frequency: {Frequency}");
        }
    }
}
