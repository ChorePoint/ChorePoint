import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { DashboardFooterMenu } from '../../../../shared/components/dashboard-footer-menu/dashboard-footer-menu';

@Component({
  selector: 'app-parent-dashboard',
  imports: [RouterModule, DashboardFooterMenu],
  templateUrl: './dashboard-layout.html',
  styleUrl: './dashboard-layout.scss',
})
export class DashboardLayout {
  private kidsService = inject(KidsService);

  kids = this.kidsService.kids$;
}
