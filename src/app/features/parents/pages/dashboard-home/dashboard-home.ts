import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Observable, map, of, switchMap } from 'rxjs';
import { ChoreCompletionService } from '../../../../core/services/chore-completion.service';
import { KidsService } from '../../../../core/services/kids.service';
import { UserService } from '../../../../core/services/user/users.service';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { ChoreSubmission } from '../../../chores/models/chore-submission';
import { User } from '../../../kids/models/user';
import { KidStats } from '../../models/kid-stats';

@Component({
  selector: 'app-dashboard-home',
  imports: [AsyncPipe, LoadingScreen],
  templateUrl: './dashboard-home.html',
  styleUrl: './dashboard-home.scss',
})
export class DashboardHome {
  private userService = inject(UserService);
  private choreCompletionService = inject(ChoreCompletionService);
  private kidsService = inject(KidsService);

  vm$!: Observable<{
    kids: User[];
    selectedKid: User | null;
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

        return this.choreCompletionService.getChoreCompletionStats(selectedKid.id).pipe(
          map((stats) => ({
            kids,
            selectedKid,
            stats,
          })),
        );
      }),
    );
  }

  selectKid(kid: User) {
    this.vm$ = this.vm$.pipe(map((vm) => ({ ...vm, selectedKid: kid })));
  }
}
