import { KidStats } from '../services/chore-submission/chore-submission.dtos';

export const DEFAULT_KID_STATS = Object.freeze<KidStats>({
  completed: 0,
  completedThisWeek: 0,
  approvalRate: 0,
  dueToday: 0,
  dueThisWeek: 0,
  weeklyCompletionPercentage: 100,
});
