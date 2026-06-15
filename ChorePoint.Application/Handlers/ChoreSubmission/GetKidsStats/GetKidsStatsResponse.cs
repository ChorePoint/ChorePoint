namespace ChorePoint.Application.Handlers.ChoreSubmission.GetKidsStats;

public record GetKidsStatsResponse(
    int CompletedTotal,
    int CompletedThisWeek,
    int ApprovalRate,
    int DueToday,
    int DueThisWeek,
    int WeeklyCompletionPercentage
);