import { Chore } from './chore';
import { ApprovalStatus } from '../../enums/parent/approval-status';

export interface ChoreSubmission {
  choreSubmissionId: number;
  choreId: number;
  userId: number;
  completedAt: Date;
  approvalStatus: ApprovalStatus;
  approvedAt: Date | null;
  approvedByUserId: number | null;
  notes: string | null;
  createdAt: Date | null;
  chore: Chore;
}
