import { ChoreSubmission } from '../../types/dtos/chore-submission';
import { RequestState } from '../../types/interfaces/request-state';

export type GetChoreSubmissionsResponse = RequestState<ChoreSubmission[]>;

export interface KidStats {
  completed: number;
  completedThisWeek: number;
  approvalRate: number;
  dueToday: number;
  dueThisWeek: number;
  weeklyCompletionPercentage: number;
}
