using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Representations;
using MediatR;

namespace ChorePoint.Application.Handlers.Chore.UpdateChore;

public record UpdateChoreCommand(
    int ChoreId,
    int? CategoryId,
    string Name,
    string Icon,
    string? Description,
    int Points,
    ChoreDifficulty Difficulty,
    ChoreFrequency Frequency,
    IReadOnlyList<AssignedKidToChore> AssignedKids
) : IRequest;
