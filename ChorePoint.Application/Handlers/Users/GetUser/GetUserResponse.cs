using ChorePoint.Domain.Entities;

namespace ChorePoint.Application.Handlers.Users.GetUser;

public record GetUserResponse(
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
    Parent Parent
);