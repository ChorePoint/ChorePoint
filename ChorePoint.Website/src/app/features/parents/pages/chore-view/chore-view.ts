import { AsyncPipe } from '@angular/common';
import { Component, computed, inject, OnInit, signal} from '@angular/core';
import { ChoreService } from '../../../../core/services/chore/chore.service';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { Chore } from '../../../../core/types/dtos/chore';
import { Kid } from '../../../../core/types/dtos/kid';
import { ChoreFrequency } from '../../../../core/types/enums/chore-frequency';
import { ChoreCardWrapper } from '../../../../shared/components/chore-card-wrapper/chore-card-wrapper';
import { Header } from '../../../../shared/components/header/header';
import { GetBonus, GetDaily, GetWeekly } from '../../../../shared/helpers/chore.helpers';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { LoadingAction, LoadingType } from '../../../../shared/types/loading-action';
import { TimeFrame } from '../../../../shared/types/timeframe';
import { KidSelectorHeader } from '../../../chores/components/kid-selector-header/kid-selector-header';
import {ToastPopup} from '../../../../shared/components/toast-popup/toast-popup';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-chore-view',
  imports: [KidSelectorHeader, LoadingScreen, AsyncPipe, ChoreCardWrapper, Header, ToastPopup],
  templateUrl: './chore-view.html',
  styleUrl: './chore-view.scss',
})
export class ChoreView {
  private choreService = inject(ChoreService);
  private kidService = inject(KidsService);

  private route = inject(ActivatedRoute);

  selectedFrequency: ChoreFrequency | null = null;

  ChoreFrequency: typeof ChoreFrequency = ChoreFrequency;
  TimeFrame: typeof TimeFrame = TimeFrame;

  loadingAction: LoadingAction | null = null;

  toastState = {
    visible: false,
    text: '✓ Changes saved',
    success: true,
  };

  vm = {
    kids: this.kidService.kids,
    kidId: signal<number | null>(-1),
    chores: this.choreService.chores,
    selectedKid: computed(() => this.getSelectedKid()),

    dailyChores: computed(() => GetDaily(this.choreService.chores())),
    weeklyChores: computed(() => GetWeekly(this.choreService.chores())),
    bonusChores: computed(() => GetBonus(this.choreService.chores())),

    kidsDictionary: computed(() =>
      Object.fromEntries(this.kidService.kids().map((k) => [k.kidId, k])),
    ),
  };

  constructor() {
    this.route.queryParamMap.subscribe(params => {
      const param = params.get('kidId');
      let kidId = null;

      if (param !== null) { kidId = Number(param) }

      this.vm.kidId.set(kidId);
    });
  }

  setKidId(kid: Kid | null) {
    this.vm.kidId.set(kid?.kidId ?? null);
  }

  getSelectedKid(): null | Kid {
    if (this.vm.kidId === null) return null;

    return this.kidService.kids().find(k => k.kidId == this.vm.kidId()) ?? null
  }

  getFilteredChores(chores: Chore[], selectedKid: Kid | null) {
    return chores.filter(
      (c) => selectedKid == null || c.assignedKids.some((ak) => ak.kidId === selectedKid.kidId),
    );
  }

  filterByFrequency(frequency: ChoreFrequency | null) {
    this.selectedFrequency = frequency;
  }

  deleteChore(chore: Chore) {
    this.loadingAction = { choreId: chore.choreId, type: LoadingType.Delete };

    this.choreService.deleteChore$(chore.choreId).subscribe();
  }

  toggleActive(activeArgs: { chore: Chore; active: boolean }) {
    const loadingType = activeArgs.active ? LoadingType.Delete : LoadingType.Activate;

    this.loadingAction = { choreId: activeArgs.chore.choreId, type: loadingType };

    const assignedKids = activeArgs.chore.assignedKids.map((ak) => {
      return {
        ...ak,
        isVisible: !activeArgs.active,
      };
    });

    this.choreService.updateChore$({ ...activeArgs.chore, assignedKids: assignedKids }).subscribe({
      next: () => {
        this.toastState = {
          ...this.toastState,
          text: '✓ Changes saved',
          success: true,
        };

        this.loadingAction = { choreId: -1, type: loadingType };
        this.showToast();
      },
      error: () => {
        this.toastState = {
          ...this.toastState,
          text: '✗ Error saving changes!',
          success: false,
        };

        this.loadingAction = { choreId: -1, type: loadingType };
        this.showToast();
      },
    });
  }

  isSelectedFrequency(frequency: ChoreFrequency) {
    return this.selectedFrequency === frequency || this.selectedFrequency === null;
  }

  showToast() {
    this.toastState.visible = true;
    setTimeout(() => {
      this.toastState.visible = false;
    }, 2000);
  }

  protected readonly Set = Set;
}
