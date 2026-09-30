using ChorePoint.Domain.Entities;
using ChorePoint.Domain.Enums;

namespace ChorePoint.Domain.Services;

public static class KidStatsCalculatorService
{
    public static int CalculateNumberOfChoresCompletedThisWeek(IReadOnlyList<ChoreSubmission> choreSubmissions, DateTime now)
    {
        return choreSubmissions.Count(cs =>
            cs.ApprovedThisWeek(now) && cs.Chore.Frequency is not ChoreFrequency.Bonus
        );
    }

    public static int CalculateSubmissionApprovalRate(IReadOnlyList<ChoreSubmission> choreSubmissions)
    {
        return choreSubmissions.Count(cs => cs.ApprovalStatus is ChoreApprovalStatus.Approved) * (100 / choreSubmissions.Count);
    }

    public static int CalculateNumberOfChoresDueToday(IReadOnlyList<KidChore> kidChores, DateTime now)
    {
        return kidChores.Select(kc => kc.DueDay).Count(dow => dow.Equals(now.DayOfWeek));
    }

    public static int CalculateNumberOfChoresDueThisWeek(IReadOnlyList<Chore> chores)
    {
        return chores.Count(c => c.Frequency is ChoreFrequency.Weekly or ChoreFrequency.Daily);
    }
}
