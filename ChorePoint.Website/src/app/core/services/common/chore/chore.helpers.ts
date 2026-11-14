import { ChoreFrequency } from '../../../types/enums/parent/chore-frequency';
import { Chore } from '../../../types/dtos/parent/chore';

export function GetDaily(chores: Chore[]) {
  return chores.filter((c) => c.frequency === ChoreFrequency.Daily);
}

export function GetWeekly(chores: Chore[]) {
  return chores.filter((c) => c.frequency === ChoreFrequency.Weekly);
}

export function GetBonus(chores: Chore[]) {
  return chores.filter((c) => c.frequency === ChoreFrequency.Bonus);
}
