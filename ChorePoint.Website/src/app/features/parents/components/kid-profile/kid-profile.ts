import { Component, inject, Input } from '@angular/core';
import {Router, RouterLink } from '@angular/router';
import { KidDetails } from '../../pages/kids-settings/types';

@Component({
  selector: 'app-kid-profile',
  imports: [RouterLink],
  templateUrl: './kid-profile.html',
  styleUrl: './kid-profile.scss',
})
export class KidProfile {
  private router = inject(Router);

  @Input() kid!: KidDetails;

  navigateToChore() {
    this.router.navigate(['/dashboard/chores'], {
      queryParams: { kidId: this.kid.kidId }
    });
  }

  navigateToShop() {
    this.router.navigate(['/dashboard/shop'], {
      queryParams: { kidId: this.kid.kidId }
    });
  }
}
