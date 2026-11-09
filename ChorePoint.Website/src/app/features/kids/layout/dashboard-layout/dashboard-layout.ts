import { Component, ElementRef, inject } from '@angular/core';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { filter } from 'rxjs';
import { DashboardFooterMenu } from '../../../../shared/components/dashboard-footer-menu/dashboard-footer-menu';

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
        setTimeout(() => {
          const content = this.elementRef.nativeElement.querySelector('.content');

          content?.scrollTo({
            top: 0,
            left: 0,
            behavior: 'instant'
          });
        });
      });
  }
}
