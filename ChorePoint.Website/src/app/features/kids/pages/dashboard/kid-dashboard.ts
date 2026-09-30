import { Component, computed, inject, OnInit } from '@angular/core';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { Header } from '../../../../shared/components/header/header';
import { RouterLink } from '@angular/router';
import { NgClass } from '@angular/common';
import { ChoreService } from '../../../../core/services/chore/chore.service';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { Kid } from '../../../../core/types/dtos/kid';

@Component({
  selector: 'app-chore-details',
  imports: [Header, RouterLink, NgClass, LoadingScreen],
  templateUrl: './kid-dashboard.html',
  styleUrl: './kid-dashboard.scss',
})
export class KidDashboard {
  private kidService = inject(KidsService);
  private choreService = inject(ChoreService);

  vm = {
    kids: this.kidService.kids,
    selectedKid: computed(() => this.getSelectedKid()),
  };

  selectedKid = this.kidService.kids()[0];
  chores = this.choreService.chores;

  getSelectedKid(): undefined | Kid {
    return this.kidService.kids().at(0);
  }
}
