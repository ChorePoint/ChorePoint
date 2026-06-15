using ChorePoint.Domain.Enums;
using MediatR;

namespace ChorePoint.Application.Handlers.Chore.CreateChore;

public record CreateChoreCommand(
    string Name,
    string Icon,
    string Description,
    int Points,
    ChoreDifficulty Difficulty,
    ChoreFrequency Frequency,
    IReadOnlyList<int> KidIds,
    IReadOnlyList<DayOfWeek>? DueDays,
    IReadOnlyList<bool> Visibilities
) : IRequest;