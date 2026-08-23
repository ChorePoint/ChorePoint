export interface Kid {
  kidId: number;
  name: string;
  age: number;
  avatar: string;
  lifetimePoints: number;
  spendablePoints: number | null;
  dayStreak: number;
  parentId: number;
}
