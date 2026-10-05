namespace ChorePoint.Application.RequestHandlers.ChoreSubmission.GetStatsByKid;

public record GetStatsByKidResponse(
    int CompletedTotal,
    int CompletedThisWeek,
    int ApprovalRate,
    int DueToday,
    int DueThisWeek,
    int WeeklyCompletionPercentage
);
