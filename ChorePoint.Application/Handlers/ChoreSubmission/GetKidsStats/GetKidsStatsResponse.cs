namespace ChorePoint.Application.Handlers.ChoreSubmission.GetKidsStats;

public record GetKidsStatsResponse(
    int Completed,
    int CompletedThisWeek,
    int ApprovalRate,
    int DueToday,
    int DueThisWeek,
    int WeeklyCompletionPercentage
);