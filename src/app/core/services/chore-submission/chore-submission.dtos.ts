import { ChoreSubmission } from '../../types/dtos/chore-submission';
import { ApiResponse } from '../dtos/response';

export type GetKidStatsResponse = ApiResponse<KidStats>;
export type GetChoreSubmissionsResponse = ApiResponse<ChoreSubmission[] | []>;

export type KidStats = {
  completed: number;
  completedThisWeek: number;
  approvalRate: number;
  dueToday: number;
  dueThisWeek: number;
  weeklyCompletionPercentage: number;
};

export const DEFAULT_KID_STATS = Object.freeze<KidStats>({
  completed: 0,
  completedThisWeek: 0,
  approvalRate: 0,
  dueToday: 0,
  dueThisWeek: 0,
  weeklyCompletionPercentage: 100,
});
