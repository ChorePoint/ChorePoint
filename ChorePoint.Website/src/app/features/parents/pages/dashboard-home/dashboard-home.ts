import { Component, inject, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { KidStats } from '../../../../core/services/chore-submission/chore-submission.dtos';
import { ChoreSubmissionService } from '../../../../core/services/chore-submission/chore-submission.service';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { Kid } from '../../../../core/types/dtos/kid';
import { Header } from '../../../../shared/components/header/header';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { KidSelectorHeader } from '../../../chores/components/kid-selector-header/kid-selector-header';
import { DashboardStats } from '../../components/dashboard-stats/dashboard-stats';
import { PendingApproval } from '../../components/pending-approval/pending-approval';

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

  vm$ = {
    kids: this.kidsService.kids$,
    selectedKid: null as Kid | null,
    pendingApprovals: this.choreSubmissionService.submissions$,
    stats: null as KidStats | null,
  };

  ngOnInit() {
    if (this.vm$.kids.length === 0) {
      this.vm$.selectedKid = this.vm$.kids()[0];
    }
  }

  onReviewComplete() {
    this.refresh$.next();
  }

  selectKid(kid: Kid) {
    this.vm$.selectedKid = kid;
  }
}
