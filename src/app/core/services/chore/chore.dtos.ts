import { Chore } from '../../types/dtos/chore';
import { ApiResponse } from '../dtos/response';

export type GetChoresResponse = ApiResponse<Chore[]>;
