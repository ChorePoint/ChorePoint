export interface IPendingApproval {
  id: number;
  choreName: string;
  completedAt: Date;
  points: number;
  icon: string;
}

export enum ButtonType {
  Reject,
  Approve,
}
