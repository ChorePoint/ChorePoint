import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Observable, combineLatest, map, of, switchMap } from 'rxjs';
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

  vm$!: Observable<{
    kids: Kid[];
    selectedKid: Kid | null;
    stats?: KidStats;
    pendingApprovals?: ChoreSubmission[];
  }>;

  ngOnInit() {
    this.loadKids();
  }

  private loadKids() {
    this.vm$ = combineLatest([
      this.kidsDataService.getKids$(),
      this.choreCompletionService.getSubmissions$(),
    ]).pipe(
      map(([kids, pendingApprovals]) => ({
        kids,
        selectedKid: kids[0] || null,
        pendingApprovals: pendingApprovals,
      })),
      switchMap(({ kids, selectedKid, pendingApprovals }) => {
        if (!selectedKid) {
          return of({
            kids,
            selectedKid: null,
            stats: undefined,
            pendingApprovals: pendingApprovals,
          });
        }

        return this.choreCompletionService.getChoreSubmissionStats(selectedKid.id).pipe(
          map((stats) => ({
            kids,
            selectedKid,
            stats,
            pendingApprovals: pendingApprovals,
          })),
        );
      }),
    );
  }

  selectKid(kid: Kid) {
    this.vm$ = this.vm$.pipe(map((vm) => ({ ...vm, selectedKid: kid })));
  }
}
