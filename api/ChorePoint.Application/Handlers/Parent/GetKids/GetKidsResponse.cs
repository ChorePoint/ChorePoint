namespace ChorePoint.Application.Handlers.Parent.GetKids;

public record GetKidsResponse(
    int KidId,
    string Name,
    string Avatar,
    int? Age,
    int DayStreak,
    int LifetimePoints,
    int SpendablePoints
);