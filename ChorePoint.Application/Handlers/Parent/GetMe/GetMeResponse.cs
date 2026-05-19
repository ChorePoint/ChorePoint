namespace ChorePoint.Application.Handlers.Parent.GetMe;

public record GetMeResponse(
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