import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NgClass } from '@angular/common';
import { LoadingScreen } from '../../common/loading-screen/loading-screen';
import { Header } from '../../../components/common/header/header';
import { Chore } from '../../../core/types/dtos/parent/chore';
import { KidIdentityService } from '../../../core/services/kid/identity/kid-identity.service';

@Component({
  selector: 'app-kid-dashboard-home',
  imports: [LoadingScreen, Header, RouterLink, NgClass],
  templateUrl: './dashboard-home.html',
  styleUrl: './dashboard-home.scss',
})
export class KidDashboardHome implements OnInit {
  private kidService = inject(KidIdentityService);

  loading = false;

  vm = {
    identity: this.kidService.identity,
    chores: signal<Chore[]>([]),
  };

  ngOnInit() {
    console.log(this.vm.identity());
  }
}
