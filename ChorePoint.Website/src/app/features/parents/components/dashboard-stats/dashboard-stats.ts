import { Component, Input } from '@angular/core';
import { IDashboardStats } from './types';

@Component({
  selector: 'app-dashboard-stats',
  imports: [],
  templateUrl: './dashboard-stats.html',
  styleUrl: './dashboard-stats.scss',
})
export class DashboardStats {
  @Input() stats!: IDashboardStats;
}
