import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';
import { ApiService } from '../../../../core/services/api.service';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { User } from '../../../kids/models/user';
import { Chore } from '../../models/chore';
import { Difficulty } from '../../models/enums/difficulty.enum';
import { Frequency } from '../../models/enums/frequency.enum';

@Component({
  selector: 'app-chore-dashboard',
  imports: [RouterLink, FontAwesomeModule, LoadingScreen],
  templateUrl: './chore-dashboard.html',
  styleUrl: './chore-dashboard.scss',
})
export class ChoreDashboard {
  private apiService = inject(ApiService);

  user: User | null = null;
  chores: Chore[] = [];

  Difficulty = Difficulty;
  Frequency = Frequency;

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
    this.user = await this.apiService.processGet('Users/me');
  }

  async loadChores() {
    if (this.user) {
      this.chores = await this.apiService.processGet(`Chore/user/${this.user.id}`);
    }
  }

  getDailyChores(): Chore[] {
    return this.chores.filter((chore) => chore.isVisible && chore.frequency == Frequency.Daily);
  }

  getWeeklyChores(): Chore[] {
    return this.chores.filter((chore) => chore.isVisible && chore.frequency == Frequency.Weekly);
  }

  getBonusChores(): Chore[] {
    return this.chores.filter((chore) => chore.isVisible && chore.frequency == Frequency.Bonus);
  }
}
