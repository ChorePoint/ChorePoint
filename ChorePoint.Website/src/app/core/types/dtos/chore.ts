import { ChoreDifficulty } from '../enums/chore-difficulty';
import { ChoreFrequency } from '../enums/chore-frequency';
import { DayOfWeek } from '../enums/day-of-week';
import { AssignedKidToChore } from './assigned-kid-to-chore';

export interface Chore {
  choreId: number;
  assignedKids: AssignedKidToChore[];
  name: string;
  icon: string;
  points: number;
  difficulty: ChoreDifficulty;
  frequency: ChoreFrequency;
  dueDay: DayOfWeek | null;
  lastCompleted: Date | null;
  createdAt: Date | null;
  updatedAt: Date | null;
  completionCount: number;
  description: string | null;
}
