import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DashboardFooterMenu } from '../../../../shared/components/dashboard-footer-menu/dashboard-footer-menu';

@Component({
  selector: 'app-kids-layout',
  imports: [RouterOutlet, DashboardFooterMenu],
  templateUrl: './kids-layout.html',
  styleUrl: './kids-layout.scss',
})
export class KidsLayout {}
