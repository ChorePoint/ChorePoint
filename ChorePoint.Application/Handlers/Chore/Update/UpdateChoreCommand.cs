using ChorePoint.Domain.Enums;
using MediatR;

namespace ChorePoint.Application.Handlers.Chore.Update;

public record UpdateChoreCommand(
    int Id,
    int KidId,
    string Name,
    string Icon,
    int Points,
    ChoreDifficulty Difficulty,
    ChoreFrequency Frequency,
    bool IsVisible,
    string Description,
    DayOfWeek DueDay
) : IRequest;