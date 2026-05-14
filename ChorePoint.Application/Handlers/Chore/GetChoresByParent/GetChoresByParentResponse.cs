using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Enums;

namespace ChorePoint.Application.Handlers.Chore.GetChoresByParent;

public record GetChoresByParentResponse(
    int Id,
    int UserId,
    string Name,
    string Icon,
    int Points,
    ChoreDifficulty Difficulty,
    ChoreFrequency Frequency,
    string TimeOfDay,
    bool IsVisible,
    DateTime? LastCompletedAt,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    int CompletionCount,
    string? Description,
    User User
);