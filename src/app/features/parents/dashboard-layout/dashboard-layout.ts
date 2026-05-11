import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { map, Observable } from 'rxjs';
import { UserService } from '../../../core/services/kids/kids.service';
import { DashboardFooterMenu } from '../../../shared/components/dashboard-footer-menu/dashboard-footer-menu';
import { Kid } from '../../kids/models/user';

@Component({
  selector: 'app-parent-dashboard',
  imports: [RouterModule, DashboardFooterMenu],
  templateUrl: './dashboard-layout.html',
  styleUrl: './dashboard-layout.scss',
})
export class DashboardLayout {
  private userService = inject(UserService);

  vm$!: Observable<{
    kids: Kid[];
  }>;

  ngOnInit() {
    this.vm$ = this.userService.getKids().pipe(map((kids) => ({ kids })));
  }
}
