import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { combineLatest, finalize, map, Observable, startWith, Subject, switchMap } from 'rxjs';
import { ChoreService } from '../../../../core/services/chore/chore.service';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { Chore } from '../../../../core/types/dtos/chore';
import { Kid } from '../../../../core/types/dtos/kid';
import { ChoreFrequency } from '../../../../core/types/enums/chore-frequency';
import { ChoreCardWrapper } from '../../../../shared/components/chore-card-wrapper/chore-card-wrapper';
import { GetBonus, GetDaily, GetWeekly } from '../../../../shared/helpers/chore.helpers';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { LoadingAction, LoadingType } from '../../../../shared/types/loading-action';
import { TimeFrame } from '../../../../shared/types/timeframe';
import { KidSelectorHeader } from '../../../chores/components/kid-selector-header/kid-selector-header';

@Component({
  selector: 'app-chore-view',
  imports: [KidSelectorHeader, LoadingScreen, AsyncPipe, RouterLink, ChoreCardWrapper],
  templateUrl: './chore-view.html',
  styleUrl: './chore-view.scss',
})
export class ChoreView implements OnInit {
  private kidService = inject(KidsService);
  private choreService = inject(ChoreService);

  private refresh$ = new Subject<void>();

  selectedFrequency: ChoreFrequency | null = null;

  ChoreFrequency: typeof ChoreFrequency = ChoreFrequency;
  TimeFrame: typeof TimeFrame = TimeFrame;

  loadingAction: LoadingAction | null = null;

  vm$!: Observable<{
    kids: Kid[];
    kidsDictionary: Record<number, Kid>;
    dailyChores: Chore[];
    weeklyChores: Chore[];
    bonusChores: Chore[];
    selectedKid: Kid | null;
  }>;

  ngOnInit() {
    this.vm$ = this.refresh$.pipe(
      startWith(void 0),
      switchMap(() =>
        combineLatest([this.kidService.getKids$(), this.choreService.getChores$()]).pipe(
          map(([kids, chores]) => ({
            kids: kids,
            kidsDictionary: kids.reduce(
              (acc, kid) => {
                acc[kid.id] = kid;
                return acc;
              },
              {} as Record<number, Kid>,
            ),
            dailyChores: GetDaily(chores),
            weeklyChores: GetWeekly(chores),
            bonusChores: GetBonus(chores),
            selectedKid: null,
          })),
          finalize(() => (this.loadingAction = null)),
        ),
      ),
    );
  }

  getFilteredChores(chores: Chore[], selectedKid: Kid | null) {
    return chores.filter((c) => selectedKid == null || c.kidId === selectedKid.id);
  }

  filterByFrequency(frequency: ChoreFrequency | null) {
    this.selectedFrequency = frequency;
  }

  deleteChore(chore: Chore) {
    this.loadingAction = { choreId: chore.id, type: LoadingType.Delete };

    this.choreService.deleteChore$(chore.id).subscribe(() => {
      this.refresh$.next();
    });
  }

  toggleActive(chore: Chore) {
    this.loadingAction = { choreId: chore.id, type: LoadingType.Activate };

    this.choreService
      .updateChore$({
        id: chore.id,
        name: chore.name,
        icon: chore.icon,
        kidId: chore.kidId,
        frequency: chore.frequency,
        dueDay: chore.dueDay,
        points: chore.points,
        description: chore.description,
        isVisible: !chore.isVisible,
      })
      .subscribe(() => {
        this.refresh$.next();
      });
  }

  isSelectedFrequency(frequency: ChoreFrequency) {
    return this.selectedFrequency === frequency || this.selectedFrequency === null;
  }
}
