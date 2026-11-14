import { AssignedKidToChore } from './assigned-kid-to-chore';
import { ChoreDifficulty } from '../../enums/parent/chore-difficulty';
import { ChoreFrequency } from '../../enums/parent/chore-frequency';
import { DayOfWeek } from '../../enums/parent/day-of-week';

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
