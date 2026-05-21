using ChoreE = ChorePoint.Domain.Entities.Chore;
using ParentE = ChorePoint.Domain.Entities.Parent;

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
    ICollection<ChoreE> Chores,
    ParentE Parent
);