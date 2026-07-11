namespace ChorePoint.Application.Handlers.Parent.GetKidsByParent;

public record GetKidsByParentResponse(
    int KidId,
    string Name,
    string Avatar,
    int? Age,
    int DayStreak,
    int LifetimePoints,
    int SpendablePoints
);
