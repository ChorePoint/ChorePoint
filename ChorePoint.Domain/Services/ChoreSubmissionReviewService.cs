using ChorePoint.Domain.Entities;

namespace ChorePoint.Domain.Services;

public static class ChoreSubmissionReviewService
{
    public static void Review(string? reviewNotes, bool approve, ChoreSubmission choreSubmission, Kid kid, Chore chore, KidChore kidChore, DateTime now)
    {
        choreSubmission.Review(reviewNotes, approve, now);

        if (!approve)
        {
            return;
        }

        kid.AddPoints(chore.Points);
        kidChore.IncreaseCompletionStreak(now);
    }
}
