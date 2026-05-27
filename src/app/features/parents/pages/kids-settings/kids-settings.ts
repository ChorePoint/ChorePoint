import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { combineLatest, Observable, switchMap } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { KidStats } from '../../../../core/services/chore-submission/chore-submission.dtos';
import { ChoreSubmissionService } from '../../../../core/services/chore-submission/chore-submission.service';
import { ChoreService } from '../../../../core/services/chore/chore.service';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { Chore } from '../../../../core/types/dtos/chore';
import { Kid } from '../../../../core/types/dtos/kid';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { KidProfile } from '../../components/kid-profile/kid-profile';
import { KidSummary } from '../../components/kid-summary/kid-summary';
import { Leaderboard } from '../../components/leaderboard/leaderboard';
import { KidDetails } from './types';

@Component({
  selector: 'app-kids-settings',
  imports: [AsyncPipe, LoadingScreen, RouterLink, Leaderboard, KidProfile, KidSummary],
  templateUrl: './kids-settings.html',
  styleUrl: './kids-settings.scss',
})
export class KidsSettings {
  private choreCompletionService = inject(ChoreSubmissionService);
  private choreService = inject(ChoreService);
  private kidService = inject(KidsService);

  vm$!: Observable<{
    kids: KidDetails[];
    summaryStats: {
      totalPoints: number;
      choresDone: number;
    };
  }>;

  ngOnInit() {
    this.vm$ = combineLatest([this.kidService.getKids$(), this.choreService.getChores$()]).pipe(
      switchMap(([kids, chores]) => {
        const kidDetails$ = kids.map((kid) => this.buildKidVm(kid, chores));

        return combineLatest(kidDetails$).pipe(
          map((kidsWithStats) => ({
            kids: kidsWithStats,
            summaryStats: this.calcSummary(kidsWithStats),
          })),
        );
      }),
    );
  }

  private buildKidVm(kid: Kid, chores: Chore[]): Observable<KidDetails> {
    return this.choreCompletionService.getChoreSubmissionStats$(kid.id).pipe(
      map((stats) => ({
        ...kid,
        chores: chores.filter((c) => c.userId === kid.id),
        kidStats: {
          ...stats!,
          weeklyCompletionPercentage: this.calcWeeklyCompletion(stats!),
        },
      })),
    );
  }

  private calcSummary(kids: KidDetails[]) {
    return {
      totalPoints: kids.reduce((sum, kid) => sum + kid.totalPoints, 0),
      choresDone: kids.reduce((sum, kid) => sum + kid.kidStats.completedThisWeek, 0),
    };
  }

  private calcWeeklyCompletion(stats: KidStats) {
    return stats.dueThisWeek > 0 ? (stats.completedThisWeek / stats.dueThisWeek) * 100 : 100;
  }
}
