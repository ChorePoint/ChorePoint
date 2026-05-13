import { ChoreDifficulty } from '../enums/chore-difficulty';
import { ChoreFrequency } from '../enums/chore-frequency';
import { DayOfWeek } from '../enums/day-of-week';

export type Chore = {
  id: number;
  userId: number;
  name: string;
  icon: string;
  points: number;
  difficulty: ChoreDifficulty;
  frequency: ChoreFrequency;
  dueDay: DayOfWeek | null;
  isVisible: boolean;
  lastCompleted: Date | null;
  createdAt: Date | null;
  updatedAt: Date | null;
  completionCount: number;
  description: string | null;
};
