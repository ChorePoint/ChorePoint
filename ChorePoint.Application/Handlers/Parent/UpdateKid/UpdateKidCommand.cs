using ChorePoint.Domain.Enums;
using MediatR;

namespace ChorePoint.Application.Handlers.Parent.UpdateKid;

public record UpdateKidCommand(
    int Id,
    string Name,
    int Age,
    string Avatar,
    int SpendablePoints,
    int DayStreak
) : IRequest;