import { FormControl } from '@angular/forms';
import { AssignedKidToChore } from '../../core/types/dtos/assigned-kid-to-chore';
import { ChoreDifficulty } from '../../core/types/enums/chore-difficulty';
import { ChoreFrequency } from '../../core/types/enums/chore-frequency';

export interface ChoreFormGroup {
  name: FormControl<string>;
  icon: FormControl<string>;
  assignedKids: FormControl<AssignedKidToChore[]>;
  frequency: FormControl<ChoreFrequency>;
  difficulty: FormControl<ChoreDifficulty>;
  points: FormControl<number>;
  description: FormControl<string | null>;
}
