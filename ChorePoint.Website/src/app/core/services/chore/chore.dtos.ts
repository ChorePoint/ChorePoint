import { AssignedKidToChore } from '../../types/dtos/assigned-kid-to-chore';
import { Chore } from '../../types/dtos/chore';
import { ApiGetResponse } from '../dtos/response';

export type GetChoresResponse = ApiGetResponse<Chore[]>;

export interface CreateChoreRequest {
  name: string;
  icon: string;
  assignedKids: AssignedKidToChore[];
  frequency: number;
  points: number;
  description: string | null;
}

export type UpdateChoreRequest = CreateChoreRequest & { id: number };
