import { Kid } from '../../../core/types/dtos/parent/kid';
import { Chore } from '../../../core/types/dtos/parent/chore';
import { KidStats } from '../../../core/services/parent/chore-submission/chore-submission.dtos';

export type KidDetails = Kid & {
  chores: Chore[];
  kidStats: KidStats;
};

export interface SummaryStats {
  totalPoints: number;
  choresDone: number;
}
