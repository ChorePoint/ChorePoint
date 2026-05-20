import { DatePipe } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { ChoreSubmissionService } from '../../../../core/services/chore-submission/chore-submission.service';
import { IPendingApproval } from './types';

@Component({
  selector: 'app-pending-approval',
  imports: [DatePipe],
  templateUrl: './pending-approval.html',
  styleUrl: './pending-approval.scss',
})
export class PendingApproval {
  private choreSubmissionService = inject(ChoreSubmissionService);

  @Input() pendingApproval!: IPendingApproval;

  success: boolean | null = null;

  approve() {
    this.choreSubmissionService.approveChore(this.pendingApproval.id).subscribe({
      next: () => {
        this.success = true;
      },
      error: (error) => {
        this.success = false;
      },
    });
  }
}
