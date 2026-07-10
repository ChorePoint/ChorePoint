import { Component, inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { map, Observable } from 'rxjs';
import { KidsService } from '../../../core/services/kids/kids.service';
import { Kid } from '../../../core/types/dtos/kid';
import { DashboardFooterMenu } from '../../../shared/components/dashboard-footer-menu/dashboard-footer-menu';

@Component({
  selector: 'app-parent-dashboard',
  imports: [RouterModule, DashboardFooterMenu],
  templateUrl: './dashboard-layout.html',
  styleUrl: './dashboard-layout.scss',
})
export class DashboardLayout implements OnInit {
  private kidsService = inject(KidsService);

  vm$!: Observable<{
    kids: Kid[] | null;
  }>;

  ngOnInit() {
    this.vm$ = this.kidsService.getKids$().pipe(map((kids) => ({ kids })));
  }
}
