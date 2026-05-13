import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Observable, map, of, switchMap } from 'rxjs';
import { KidStats } from '../../../../core/services/chore-submission/chore-submission.dtos';
import { ChoreSubmissionService } from '../../../../core/services/chore-submission/chore-submission.service';
import { KidsService } from '../../../../core/services/kids/kids-data.service';
import { ChoreSubmission } from '../../../../core/types/dtos/chore-submission';
import { Kid } from '../../../../core/types/dtos/kid';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';

@Component({
  selector: 'app-dashboard-home',
  imports: [AsyncPipe, LoadingScreen],
  templateUrl: './dashboard-home.html',
  styleUrl: './dashboard-home.scss',
})
export class DashboardHome {
  private choreCompletionService = inject(ChoreSubmissionService);
  private kidsService = inject(KidsService);

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
    this.vm$ = this.kidsService.getKids$().pipe(
      map((kids) => ({
        kids,
        selectedKid: kids[0] || null,
      })),
      switchMap(({ kids, selectedKid }) => {
        if (!selectedKid) {
          return of({
            kids,
            selectedKid: null,
            stats: undefined,
          });
        }

        return this.choreCompletionService.getChoreSubmissionStats(selectedKid.id).pipe(
          map((stats) => ({
            kids,
            selectedKid,
            stats,
          })),
        );
      }),
    );
  }

  selectKid(kid: Kid) {
    this.vm$ = this.vm$.pipe(map((vm) => ({ ...vm, selectedKid: kid })));
  }
}
