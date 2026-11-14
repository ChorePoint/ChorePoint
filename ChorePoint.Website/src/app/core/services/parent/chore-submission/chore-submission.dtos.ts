import { ChoreSubmission } from '../../../types/dtos/parent/chore-submission';
import { RequestState } from '../../../types/interfaces/common/request-state';

export type GetChoreSubmissionsResponse = RequestState<ChoreSubmission[]>;

export interface KidStats {
  completed: number;
  completedThisWeek: number;
  approvalRate: number;
  dueToday: number;
  dueThisWeek: number;
  weeklyCompletionPercentage: number;
}
