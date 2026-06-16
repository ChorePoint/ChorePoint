using ChorePoint.Domain.Enums;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByKid;

public record GetChoresByKidResponse(
    int ChoreId,
    int ParentId,
    string Name,
    string Icon,
    string? Description,
    int Points,
    ChoreDifficulty Difficulty,
    ChoreFrequency Frequency,
    DateTime? LastCompletedAt,
    int CompletionCount,
    DayOfWeek? DueDay,
    bool IsVisible
);