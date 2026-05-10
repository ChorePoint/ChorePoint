export type CreateChoreRequest = {
  name: string;
  icon: string;
  kidId: number;
  frequency: number;
  dueDay: number | null;
  points: number;
  description: string;
};
