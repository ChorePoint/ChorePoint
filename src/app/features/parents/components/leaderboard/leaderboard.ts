import { Component, Input } from '@angular/core';
import { Kid } from '../../../kids/models/user';

@Component({
  selector: 'app-leaderboard',
  imports: [],
  templateUrl: './leaderboard.html',
  styleUrl: './leaderboard.scss',
})
export class Leaderboard {
  @Input() kids: Kid[] = [];

  leaderboardKids: Kid[] = [];
  rankClasses = ['gold', 'silver', 'bronze'];
  rankEmojis = ['🥇', '🥈', '🥉'];

  ngOnInit(): void {
    this.leaderboardKids = this.kids.sort((a, b) => b.totalPoints - a.totalPoints).slice(0, 3);
  }
}
