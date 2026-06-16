using ChorePoint.Domain.Enums;
using ChorePoint.Domain.Representations;
using MediatR;

namespace ChorePoint.Application.Handlers.Chore.CreateChore;

public record CreateChoreCommand(
    string Name,
    string Icon,
    string Description,
    int Points,
    ChoreDifficulty Difficulty,
    ChoreFrequency Frequency,
    IReadOnlyList<AssignedKidToChore> AssignedKids
) : IRequest;