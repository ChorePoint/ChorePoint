import { FormControl } from '@angular/forms';
import { AssignedKidToChore } from '../../dtos/parent/assigned-kid-to-chore';
import { ChoreFrequency } from '../../enums/parent/chore-frequency';
import { ChoreDifficulty } from '../../enums/parent/chore-difficulty';

export interface ChoreFormGroup {
  name: FormControl<string>;
  icon: FormControl<string>;
  assignedKids: FormControl<AssignedKidToChore[]>;
  frequency: FormControl<ChoreFrequency>;
  difficulty: FormControl<ChoreDifficulty>;
  points: FormControl<number>;
  description: FormControl<string | null>;
}
