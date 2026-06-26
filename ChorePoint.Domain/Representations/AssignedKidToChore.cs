namespace ChorePoint.Domain.Representations;

public record AssignedKidToChore(
    int KidId,
    DayOfWeek? DueDay,
    bool IsVisible
);