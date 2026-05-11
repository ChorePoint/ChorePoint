import { ChoreDifficulty } from '../../../core/types/enums/chore-difficulty';
import { ChoreFrequency } from '../../../core/types/enums/chore-frequency';

export type Chore = {
  id: number;
  userId: number;
  name: string;
  icon: string;
  points: number;
  difficulty: ChoreDifficulty;
  frequency: ChoreFrequency;
  timeOfDay: string;
  isVisible: boolean;
  lastCompletedAt: Date | null;
  createdAt: Date;
  updatedAt: Date;
  completionCount: number;
  description: string;
};
