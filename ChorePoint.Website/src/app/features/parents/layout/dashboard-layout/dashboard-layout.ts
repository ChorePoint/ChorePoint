import { Component, ElementRef, inject } from '@angular/core';
import {NavigationEnd, Router, RouterModule } from '@angular/router';
import { DashboardFooterMenu } from '../../../../shared/components/dashboard-footer-menu/dashboard-footer-menu';
import {filter} from 'rxjs';

@Component({
  selector: 'app-parent-dashboard',
  imports: [RouterModule, DashboardFooterMenu],
  templateUrl: './dashboard-layout.html',
  styleUrl: './dashboard-layout.scss',
})
export class DashboardLayout {
  private router = inject(Router);
  private elementRef = inject(ElementRef);

  constructor() {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        const content = this.elementRef.nativeElement.querySelector('.content');

        content?.scrollTo({
          top: 0,
          behavior: 'instant'
        });
      });
  }
}
