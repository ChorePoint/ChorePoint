import { DatePipe } from '@angular/common';
import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { ChoreSubmissionService } from '../../../../core/services/chore-submission/chore-submission.service';
import { LoadingEmoji } from '../../../../shared/components/loading-emoji/loading-emoji';
import { ButtonType, IPendingApproval } from './types';

@Component({
  selector: 'app-pending-approval',
  imports: [DatePipe, LoadingEmoji],
  templateUrl: './pending-approval.html',
  styleUrl: './pending-approval.scss',
})
export class PendingApproval {
  private choreSubmissionService = inject(ChoreSubmissionService);

  @Input() pendingApproval!: IPendingApproval;
  @Input() loading = false;

  @Output() refresh = new EventEmitter<void>();

  loadingButton: ButtonType | null = null;

  ButtonType: typeof ButtonType = ButtonType;

  review(approve: boolean) {
    this.loading = true;
    this.loadingButton = approve ? ButtonType.Approve : ButtonType.Reject;

    this.choreSubmissionService.reviewChore$(this.pendingApproval.id, approve).subscribe({
      next: () => {
        this.refresh.emit();
      },
    });
  }
}
