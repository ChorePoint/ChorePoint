namespace ChorePoint.Application.Handlers.ChoreSubmission.GetKidsStats;

public record GetKidsStatsResponse(
    int CompletedThisWeek,
    int ApprovalRate
);