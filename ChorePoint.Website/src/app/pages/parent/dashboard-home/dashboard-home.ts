import { Component, inject, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { DashboardStats } from '../../../components/parent/dashboard-stats/dashboard-stats';
import { LoadingScreen } from '../../common/loading-screen/loading-screen';
import { PendingApproval } from '../../../components/parent/pending-approval/pending-approval';
import { KidSelectorHeader } from '../../../components/parent/chores/kid-selector-header/kid-selector-header';
import { Header } from '../../../components/common/header/header';
import { ChoreSubmissionService } from '../../../core/services/parent/chore-submission/chore-submission.service';
import { KidsService } from '../../../core/services/parent/kids/kids.service';
import { Kid } from '../../../core/types/dtos/parent/kid';
import { KidStats } from '../../../core/services/parent/chore-submission/chore-submission.dtos';

@Component({
  selector: 'app-dashboard-home',
  imports: [LoadingScreen, DashboardStats, PendingApproval, KidSelectorHeader, Header],
  templateUrl: './dashboard-home.html',
  styleUrl: './dashboard-home.scss',
})
export class DashboardHome implements OnInit {
  private choreSubmissionService = inject(ChoreSubmissionService);
  private kidsService = inject(KidsService);

  private refresh$ = new Subject<void>();

  loading = false;

  vm = {
    kids: this.kidsService.kids,
    selectedKid: null as Kid | null,
    pendingApprovals: this.choreSubmissionService.submissions,
    stats: null as KidStats | null,
  };

  ngOnInit() {
    if (this.vm.kids().length === 0) {
      this.vm.selectedKid = this.vm.kids()[0];
    }
  }

  onReviewComplete() {
    this.refresh$.next();
  }

  selectKid(kid: Kid) {
    this.vm.selectedKid = kid;
  }
}
