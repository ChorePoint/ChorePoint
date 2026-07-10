import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faArrowLeft, faSpinner } from '@fortawesome/free-solid-svg-icons';
import { catchError, combineLatest, map, merge, Observable, of, Subject, switchMap } from 'rxjs';
import { ChoreSubmissionService } from '../../../../core/services/chore-submission/chore-submission.service';
import { ChoreService } from '../../../../core/services/chore/chore.service';
import { ActionButton } from '../../../../core/types/action-button';
import { Chore } from '../../../../core/types/dtos/chore';
import { ChoreSubmission } from '../../../../core/types/dtos/chore-submission';
import { ChoreDifficulty } from '../../../../core/types/enums/chore-difficulty';
import { ChoreFrequency } from '../../../../core/types/enums/chore-frequency';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { APPROVAL_STATUS_CONFIG, DEFAULT_STATUS } from './approval-status.config';

@Component({
  selector: 'app-chore-details',
  imports: [FontAwesomeModule, LoadingScreen, AsyncPipe],
  templateUrl: './chore-details.html',
  styleUrl: './chore-details.scss',
})
export class ChoreDetails implements OnInit {
  private choreService = inject(ChoreService);
  private choreSubmissionService = inject(ChoreSubmissionService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  private completeChore$ = new Subject<void>();

  vm$!: Observable<{
    actionButton: ActionButton;
    chore: Chore | null;
    choreSubmission: ChoreSubmission | null;
  }>;

  error: string | null = null;

  spinner = faSpinner;
  backArrow = faArrowLeft;

  Frequency = ChoreFrequency;
  Difficulty = ChoreDifficulty;

  ngOnInit() {
    this.initChore();
  }

  private initChore() {
    const id = this.getChoreIdFromRoute();
    if (id) this.loadChore(id);
  }

  private getChoreIdFromRoute(): number | null {
    const choreId = this.route.snapshot.paramMap.get('id');
    return choreId ? +choreId : null;
  }

  private loadChore(id: number) {
    const load$ = of(null);

    const complete$ = this.completeChore$.pipe(
      switchMap(() => this.choreSubmissionService.completeChore$(id)),
    );

    this.vm$ = merge(load$, complete$).pipe(
      switchMap(() =>
        combineLatest([this.choreService.getById$(id), this.choreSubmissionService.getCurrent$(1)]),
      ),
      map(([chore, choreSubmission]) => {
        const status = choreSubmission?.approvalStatus;

        const config = status ? (APPROVAL_STATUS_CONFIG[status] ?? DEFAULT_STATUS) : DEFAULT_STATUS;

        return {
          actionButton: config,
          chore,
          choreSubmission,
        };
      }),
      catchError((error) => {
        if (error.name === 'TimeoutError') {
          console.error('Request timed out');
        } else {
          console.error('Error loading chore details:', error);
        }
        return of({ actionButton: DEFAULT_STATUS, chore: null, choreSubmission: null });
      }),
    );
  }

  backToDashboard() {
    this.router.navigate(['/dashboard']);
  }

  markComplete() {
    this.completeChore$.next();
  }
}
