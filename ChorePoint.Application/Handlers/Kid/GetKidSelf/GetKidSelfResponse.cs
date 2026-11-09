namespace ChorePoint.Application.Handlers.Kid.GetKidSelf;

public record GetKidSelfResponse(
    string Name,
    string Avatar,
    int? Age,
    int DayStreak,
    int LifetimePoints,
    int SpendablePoints
);
