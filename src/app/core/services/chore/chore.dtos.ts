import { Chore } from '../../types/dtos/chore';
import { ApiResponse } from '../dtos/response';

export type GetChoresResponse = ApiResponse<Chore[]>;

export type CreateChoreRequest = {
  name: string;
  icon: string;
  kidId: number;
  frequency: number;
  dueDay: number | null;
  points: number;
  description: string;
};
