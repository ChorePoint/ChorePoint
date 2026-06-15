using ChorePoint.Domain.Enums;
using MediatR;

namespace ChorePoint.Application.Handlers.Chore.UpdateChore;

public record UpdateChoreCommand(
    int ChoreId,
    string Name,
    string Icon,
    string Description,
    int Points,
    ChoreDifficulty Difficulty,
    ChoreFrequency Frequency,
    int KidId,
    DayOfWeek DueDay,
    bool IsVisible
) : IRequest;