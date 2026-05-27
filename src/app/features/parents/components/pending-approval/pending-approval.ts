import { DatePipe } from '@angular/common';
import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { ChoreSubmissionService } from '../../../../core/services/chore-submission/chore-submission.service';
import { LoadingEmoji } from '../../../../shared/components/loading-emoji/loading-emoji';
import { IPendingApproval } from './types';

@Component({
  selector: 'app-pending-approval',
  imports: [DatePipe, LoadingEmoji],
  templateUrl: './pending-approval.html',
  styleUrl: './pending-approval.scss',
})
export class PendingApproval {
  private choreSubmissionService = inject(ChoreSubmissionService);

  @Input() pendingApproval!: IPendingApproval;

  @Output() refresh = new EventEmitter<void>();

  loading = false;

  review(approve: boolean) {
    this.loading = true;

    this.choreSubmissionService.reviewChore$(this.pendingApproval.id, approve).subscribe({
      next: () => {
        this.loading = false;
        this.refresh.emit();
      },
      error: () => {
        this.loading = false;
      },
    });
  }
}
