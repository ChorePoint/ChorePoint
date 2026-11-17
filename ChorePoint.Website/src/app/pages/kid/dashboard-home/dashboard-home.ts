import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoadingScreen } from '../../common/loading-screen/loading-screen';
import { Header } from '../../../components/common/header/header';
import { KidIdentityService } from '../../../core/services/kid/identity/kid-identity.service';
import { CommonChoreService } from '../../../core/services/common/chore/chore.service';
import { ChoreFrequency } from '../../../core/types/enums/parent/chore-frequency';

@Component({
  selector: 'app-kid-dashboard-home',
  imports: [LoadingScreen, Header, RouterLink],
  templateUrl: './dashboard-home.html',
  styleUrl: './dashboard-home.scss',
})
export class KidDashboardHome {
  private kidService = inject(KidIdentityService);
  private choreService = inject(CommonChoreService);

  loading = false;

  vm = {
    identity: this.kidService.identity,
    chores: this.choreService.chores,
  };

  getChoresToCompleteToday() {
    const today = new Date().getDay();

    return this.vm
      .chores()
      .filter((chore) => chore.frequency == ChoreFrequency.Daily || chore.dueDay == today);
  }
}
