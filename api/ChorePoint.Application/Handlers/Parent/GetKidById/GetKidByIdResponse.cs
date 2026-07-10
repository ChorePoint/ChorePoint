namespace ChorePoint.Application.Handlers.Parent.GetKidById;

public record GetKidByIdResponse(
    int KidId,
    string Name,
    string Avatar,
    int? Age,
    int DayStreak,
    int LifetimePoints,
    int SpendablePoints
);
