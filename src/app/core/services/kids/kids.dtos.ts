import { Kid } from '../../../features/kids/models/user';

export type GetKidsResponse = {
  success: boolean;
  message: string;
  data: Kid[];
};
