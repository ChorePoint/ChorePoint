import { Chore } from '../../types/dtos/chore';
import { ApiGetResponse } from '../dtos/response';

export type GetChoresResponse = ApiGetResponse<Chore[]>;
export type CreateChoreRequest = {
  name: string;
  icon: string;
  kidId: number;
  frequency: number;
  dueDay: number | null;
  points: number;
  description: string;
};
