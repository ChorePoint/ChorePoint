export interface UpdateKidRequest {
  id: number;
  name: string;
  age: number;
  avatar: string;
  spendablePoints: number;
  dayStreak: number;
}

export interface CreateKidRequest {
  name: string;
  age: string;
  avatar: string;
}
