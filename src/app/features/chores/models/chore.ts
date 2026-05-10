import { Difficulty } from './enums/difficulty.enum';
import { Frequency } from './enums/frequency.enum';

export type Chore = {
  id: number;
  userId: number;
  name: string;
  icon: string;
  points: number;
  difficulty: Difficulty;
  frequency: Frequency;
  timeOfDay: string;
  isVisible: boolean;
  lastCompletedAt: Date | null;
  createdAt: Date;
  updatedAt: Date;
  completionCount: number;
  description: string;
};
