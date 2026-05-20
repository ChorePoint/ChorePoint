namespace ChorePoint.Application.Handlers.Parent.GetKids;

public record GetKidsResponse(
    int Id,
    string Name,
    string Avatar,
    int? Age,
    int DayStreak,
    int TotalPoints,
    int PointsToday,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    int ParentId,
    ICollection<Domain.Entities.Chore> Chores,
    Domain.Entities.Parent Parent
);