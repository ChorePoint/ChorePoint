import { KidStats } from '../../../../core/services/chore-submission/chore-submission.dtos';
import { Chore } from '../../../../core/types/dtos/chore';
import { Kid } from '../../../../core/types/dtos/kid';

export type KidDetails = Kid & {
  chores: Chore[];
  kidStats: KidStats;
};
