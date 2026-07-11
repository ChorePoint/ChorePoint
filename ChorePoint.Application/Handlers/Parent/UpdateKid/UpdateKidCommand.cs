using MediatR;

namespace ChorePoint.Application.Handlers.Parent.UpdateKid;

public record UpdateKidCommand(
    int KidId,
    string Name,
    string Avatar,
    int? Age,
    int DayStreak,
    int LifetimePoints,
    int SpendablePoints
) : IRequest;
