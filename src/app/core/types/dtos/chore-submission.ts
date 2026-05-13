import { ApprovalStatus } from '../enums/approval-status';

export type ChoreSubmission = {
  id: number;
  choreId: number;
  userId: number;
  completedAt: Date;
  approvalStatus: ApprovalStatus;
  approvedAt: Date | null;
  approvedByUserId: number | null;
  notes: string | null;
  createdAt: Date | null;
};
