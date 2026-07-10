using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Representations;

namespace ChorePoint.Application.Handlers.Chore.GetChoreById;

public record GetChoreByIdResponse(
    int ChoreId,
    string Name,
    string Icon,
    string? Description,
    int Points,
    ChoreDifficulty Difficulty,
    ChoreFrequency Frequency,
    DateTime? LastCompletedAt,
    int CompletionCount,
    Category? Category,
    IReadOnlyList<AssignedKidToChore> AssignedKids
);
