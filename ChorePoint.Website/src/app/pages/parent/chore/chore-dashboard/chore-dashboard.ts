import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { LoadingScreen } from '../../../common/loading-screen/loading-screen';
import { Kid } from '../../../../core/types/dtos/parent/kid';
import { Chore } from '../../../../core/types/dtos/parent/chore';
import { ChoreDifficulty } from '../../../../core/types/enums/parent/chore-difficulty';
import { ChoreFrequency } from '../../../../core/types/enums/parent/chore-frequency';

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
    this.loading = false;

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
    return this.chores.filter((chore) => chore.frequency == ChoreFrequency.Daily);
  }

  getWeeklyChores(): Chore[] {
    return this.chores.filter((chore) => chore.frequency == ChoreFrequency.Weekly);
  }

  getBonusChores(): Chore[] {
    return this.chores.filter((chore) => chore.frequency == ChoreFrequency.Bonus);
  }
}
