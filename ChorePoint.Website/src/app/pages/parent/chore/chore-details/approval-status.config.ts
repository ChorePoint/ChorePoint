import { ApprovalStatus } from '../../../../core/types/enums/parent/approval-status';
import { ActionButton } from '../../../../core/types/interfaces/parent/action-button';

export const APPROVAL_STATUS_CONFIG: Record<ApprovalStatus, ActionButton> = {
  [ApprovalStatus.Approved]: {
    text: 'Chore Complete!',
    class: 'btn-success',
  },
  [ApprovalStatus.Pending]: {
    text: 'Pending Approval',
    class: 'btn-warning',
  },
  [ApprovalStatus.Rejected]: {
    text: 'Rejected',
    class: 'btn-danger',
  },
  [ApprovalStatus.Incomplete]: {
    text: 'Mark Complete!',
    class: 'btn-primary',
  },
};

export const DEFAULT_STATUS = APPROVAL_STATUS_CONFIG[ApprovalStatus.Incomplete];
