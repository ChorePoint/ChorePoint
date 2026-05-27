import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Observable, Subject, combineLatest, map, startWith, switchMap } from 'rxjs';
import { KidStats } from '../../../../core/services/chore-submission/chore-submission.dtos';
import { ChoreSubmissionService } from '../../../../core/services/chore-submission/chore-submission.service';
import { KidsDataService } from '../../../../core/services/kids/kids-data.service';
import { ChoreSubmission } from '../../../../core/types/dtos/chore-submission';
import { Kid } from '../../../../core/types/dtos/kid';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { KidSelectorHeader } from '../../../chores/components/kid-selector-header/kid-selector-header';
import { DashboardStats } from '../../components/dashboard-stats/dashboard-stats';
import { PendingApproval } from '../../components/pending-approval/pending-approval';

@Component({
  selector: 'app-dashboard-home',
  imports: [AsyncPipe, LoadingScreen, DashboardStats, PendingApproval, KidSelectorHeader],
  templateUrl: './dashboard-home.html',
  styleUrl: './dashboard-home.scss',
})
export class DashboardHome {
  private choreCompletionService = inject(ChoreSubmissionService);
  private kidsDataService = inject(KidsDataService);

  private refresh$ = new Subject<void>();

  vm$!: Observable<{
    kids: Kid[];
    selectedKid: Kid | null;
    stats?: KidStats;
    pendingApprovals?: ChoreSubmission[];
  }>;

  ngOnInit() {
    // This ties the refresh signal to data fetching so that whenever refresh is
    //  triggered, the latest data is fetched and emitted to the vm$ observable.
    this.vm$ = this.refresh$.pipe(
      startWith(void 0),
      switchMap(() =>
        combineLatest([
          this.kidsDataService.getKids$(),
          this.choreCompletionService.getSubmissions$(true),
        ]),
      ),
      map(([kidsState, submissionsState]) => ({
        kids: kidsState ?? [],
        selectedKid: kidsState[0] ?? null,
        pendingApprovals: submissionsState ?? [],
      })),
    );
  }

  refresh() {
    this.refresh$.next();
  }

  selectKid(kid: Kid) {
    this.vm$ = this.vm$.pipe(map((vm) => ({ ...vm, selectedKid: kid })));
  }
}
