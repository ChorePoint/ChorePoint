import { Component } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-dashboard-footer-menu',
  imports: [RouterLink, RouterModule],
  templateUrl: './dashboard-footer-menu.html',
  styleUrl: './dashboard-footer-menu.scss',
})
export class DashboardFooterMenu {}
