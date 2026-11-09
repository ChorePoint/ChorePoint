import { Component, inject, OnInit, signal } from '@angular/core';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { Header } from '../../../../shared/components/header/header';
import { RouterLink } from '@angular/router';
import { NgClass } from '@angular/common';
import { KidsService } from '../../../../core/kid/identity/kid-identity.service';
import { Chore } from '../../../../core/types/dtos/chore';

@Component({
  selector: 'app-kid-dashboard-home',
  imports: [LoadingScreen, Header, RouterLink, NgClass],
  templateUrl: './dashboard-home.html',
  styleUrl: './dashboard-home.scss',
})
export class KidDashboardHome implements OnInit {
  private kidService = inject(KidsService);

  loading = false;

  vm = {
    identity: this.kidService.identity,
    chores: signal<Chore[]>([]),
  };

  ngOnInit() {
    console.log(this.vm.identity());
  }
}
