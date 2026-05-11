using ChorePoint.Domain.Enums;
using MediatR;

namespace ChorePoint.Application.Handlers.Chore.Create;

public record CreateChoreCommand(
    string Name,
    string Icon,
    int KidId,
    ChoreFrequency Frequency,
    DayOfWeek? DueDay,
    int Points,
    ChoreDifficulty Difficulty,
    string Description
) : IRequest;