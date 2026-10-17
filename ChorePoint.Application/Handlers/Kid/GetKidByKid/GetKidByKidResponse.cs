namespace ChorePoint.Application.Handlers.Kid.GetKidByKid;

public record GetKidByKidResponse(
    string Name,
    string Avatar,
    int? Age,
    int DayStreak,
    int LifetimePoints,
    int SpendablePoints
);
