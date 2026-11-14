import { ChoreFrequency } from '../../types/enums/parent/chore-frequency';

export const FREQUENCY_OPTIONS = [
  { value: ChoreFrequency.Daily, label: 'Daily' },
  { value: ChoreFrequency.Weekly, label: 'Weekly' },
  { value: ChoreFrequency.Bonus, label: 'Bonus' },
];
