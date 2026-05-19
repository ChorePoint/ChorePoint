using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Enums;

namespace ChorePoint.Application.Handlers.ChoreSubmission.GetCurrent;

public record GetCurrentResponse(
    int Id,
    int KidId,
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
    Kid Kid
);