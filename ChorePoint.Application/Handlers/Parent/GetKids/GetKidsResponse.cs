using ChoreE = ChorePoint.Domain.Entities.Chore;
using ParentE = ChorePoint.Domain.Entities.Parent;

namespace ChorePoint.Application.Handlers.Parent.GetKids;

public record GetKidsResponse(
    int KidId,
    string Name,
    string Avatar,
    int? Age,
    int DayStreak,
    int LifetimePoints,
    int SpendablePoints,
    ParentE Parent,
    ICollection<ChoreE> Chores
);