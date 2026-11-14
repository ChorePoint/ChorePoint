import { ApiGetResponse } from '../../common/dtos/response';
import { Chore } from '../../../types/dtos/parent/chore';
import { AssignedKidToChore } from '../../../types/dtos/parent/assigned-kid-to-chore';

export type GetChoresResponse = ApiGetResponse<Chore[]>;

export interface CreateChoreRequest {
  name: string;
  icon: string;
  assignedKids: AssignedKidToChore[];
  frequency: number;
  points: number;
  description: string | null;
}

export type UpdateChoreRequest = CreateChoreRequest & { choreId: number };
