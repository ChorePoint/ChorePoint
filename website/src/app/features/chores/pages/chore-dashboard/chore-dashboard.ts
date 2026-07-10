import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { Chore } from '../../../../core/types/dtos/chore';
import { Kid } from '../../../../core/types/dtos/kid';
import { ChoreDifficulty } from '../../../../core/types/enums/chore-difficulty';
import { ChoreFrequency } from '../../../../core/types/enums/chore-frequency';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';

@Component({
  selector: 'app-chore-dashboard',
  imports: [RouterLink, FontAwesomeModule, LoadingScreen],
  templateUrl: './chore-dashboard.html',
  styleUrl: './chore-dashboard.scss',
})
export class ChoreDashboard implements OnInit {
  user: Kid | null = null;
  chores: Chore[] = [];

  Difficulty = ChoreDifficulty;
  Frequency = ChoreFrequency;

  loading = true;
  spinner = faSpinner;

  ngOnInit() {
    this.loadUser().then(() => {
      this.loadChores().then(() => {
        this.loading = false;
      });
    });
  }

  async loadUser() {
    // this.user = await this.apiService.processGet('Users/me');
  }

  async loadChores() {
    if (this.user) {
      // this.chores = await this.apiService.processGet(`Chore/user/${this.user.id}`);
    }
  }

  getDailyChores(): Chore[] {
    return this.chores.filter(
      (chore) => chore.isVisible && chore.frequency == ChoreFrequency.Daily,
    );
  }

  getWeeklyChores(): Chore[] {
    return this.chores.filter(
      (chore) => chore.isVisible && chore.frequency == ChoreFrequency.Weekly,
    );
  }

  getBonusChores(): Chore[] {
    return this.chores.filter(
      (chore) => chore.isVisible && chore.frequency == ChoreFrequency.Bonus,
    );
  }
}
