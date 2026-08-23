import { Component, computed, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DEFAULT_KID_STATS } from '../../../../core/consts/default-kid-stats';
import { ChoreSubmissionService } from '../../../../core/services/chore-submission/chore-submission.service';
import { ChoreService } from '../../../../core/services/chore/chore.service';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { Header } from '../../../../shared/components/header/header';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { KidProfile } from '../../components/kid-profile/kid-profile';
import { KidSummary } from '../../components/kid-summary/kid-summary';
import { Leaderboard } from '../../components/leaderboard/leaderboard';
import { KidDetails } from './types';

@Component({
  selector: 'app-kids-settings',
  imports: [LoadingScreen, RouterLink, Leaderboard, KidProfile, KidSummary, Header],
  templateUrl: './kids-settings.html',
  styleUrl: './kids-settings.scss',
})
export class KidsSettings implements OnInit {
  private choreCompletionService = inject(ChoreSubmissionService);
  private choreService = inject(ChoreService);
  private kidService = inject(KidsService);

  readonly vm = computed(() => {
    const chores = this.choreService.chores();

    const kidDetails = this.kidService.kids().map((kid) => ({
      ...kid,
      chores: chores.filter((c) => c.assignedKids.some((ak) => ak.kidId === kid.kidId)),
      kidStats:
        this.choreCompletionService.getChoreSubmissionStats(kid.kidId)() ?? DEFAULT_KID_STATS,
    }));

    return {
      kidDetails,
      summaryStats: this.calcSummary(kidDetails),
    };
  });

  ngOnInit() {
    this.kidService.kids().forEach((kid) => {
      this.choreCompletionService.loadChoreSubmissionStats(kid.kidId);
    });
  }

  private calcSummary(kids: KidDetails[]) {
    return {
      totalPoints: kids.reduce((sum, kid) => sum + kid.lifetimePoints, 0),
      choresDone: kids.reduce((sum, kid) => sum + kid.kidStats.completedThisWeek, 0),
    };
  }
}
