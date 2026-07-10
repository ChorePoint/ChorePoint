import { Chore } from '../../core/types/dtos/chore';
import { ChoreFrequency } from '../../core/types/enums/chore-frequency';

export function GetDaily(chores: Chore[]) {
  return chores.filter((c) => c.frequency === ChoreFrequency.Daily);
}

export function GetWeekly(chores: Chore[]) {
  return chores.filter((c) => c.frequency === ChoreFrequency.Weekly);
}

export function GetBonus(chores: Chore[]) {
  return chores.filter((c) => c.frequency === ChoreFrequency.Bonus);
}
