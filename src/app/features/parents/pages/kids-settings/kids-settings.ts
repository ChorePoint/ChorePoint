import { AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { combineLatest, Observable } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { ChoreService } from '../../../../core/services/chore/chore.service';
import { UserService } from '../../../../core/services/kids/kids.service';
import { Chore } from '../../../../core/types/dtos/chore';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { Leaderboard } from '../../components/leaderboard/leaderboard';
import { KidDetails } from './types';

@Component({
  selector: 'app-kids-settings',
  imports: [AsyncPipe, LoadingScreen, RouterLink, Leaderboard],
  templateUrl: './kids-settings.html',
  styleUrl: './kids-settings.scss',
})
export class KidsSettings {
  private choreService = inject(ChoreService);
  private userService = inject(UserService);

  vm$!: Observable<{
    kids: KidDetails[];
    chores: Chore[];
  }>;

  ngOnInit() {
    this.vm$ = combineLatest([this.userService.getKids(), this.choreService.getChores()]).pipe(
      map(([kids, chores]) => {
        console.log('kids:', kids);
        console.log('chores:', chores);

        return {
          kids: kids.map((kid) => ({
            ...kid,
            activeChores: chores.filter((c) => c.userId === kid.id).length,
          })),
          chores,
        };
      }),
    );
  }
}
