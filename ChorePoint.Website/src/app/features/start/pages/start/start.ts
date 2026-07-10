import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.service';
import { CreateProfile } from '../create-profile/create-profile';
import { GetStarted } from '../get-started/get-started';
import { HowItWorks } from '../how-it-works/how-it-works';
import { Welcome } from '../welcome/welcome';

@Component({
  selector: 'app-start',
  imports: [HowItWorks, Welcome, CreateProfile, GetStarted],
  templateUrl: './start.html',
  styleUrl: './start.scss',
})
export class Start implements OnInit {
  private authService = inject(AuthService);
  private router = inject(Router);

  slide = 1;
  totalSlides = 4;

  ngOnInit() {
    if (this.authService.hasSeenOnboarding()) {
      this.router.navigate(['/dashboard']);
    }
  }

  nextSlide() {
    if (this.slide < this.totalSlides) {
      this.slide++;
    } else {
      this.finishOnboarding();
    }
  }

  prevSlide() {
    if (this.slide > 1) this.slide--;
  }

  skipTutorial() {
    this.finishOnboarding();
  }

  finishOnboarding() {
    this.authService.setOnboardingSeen();
    this.router.navigate(['/dashboard']);
  }
}
