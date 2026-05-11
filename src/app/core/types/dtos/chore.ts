import { ChoreDifficulty } from '../enums/chore-difficulty';
import { ChoreFrequency } from '../enums/chore-frequency';
import { DayOfWeek } from '../enums/day-of-week';

export type Chore = {
  Id: number;
  UserId: number;
  Name: string;
  Icon: string;
  Points: number;
  Difficulty: ChoreDifficulty;
  Frequency: ChoreFrequency;
  DueDay: DayOfWeek | null;
  IsVisible: boolean;
  LastCompleted: Date | null;
  CreatedAt: Date | null;
  UpdatedAt: Date | null;
  CompletionCount: number;
  Description: string | null;
};
