namespace ChorePoint.Application.Handlers.ChoreSubmission.GetStatsByKid;

public record GetStatsByKidResponse(
    int CompletedTotal,
    int CompletedThisWeek,
    int ApprovalRate,
    int DueToday,
    int DueThisWeek,
    int WeeklyCompletionPercentage
);