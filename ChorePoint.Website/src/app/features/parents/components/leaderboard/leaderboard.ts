import { Component, Input, OnInit } from '@angular/core';
import { Kid } from '../../../../core/types/dtos/kid';

@Component({
  selector: 'app-leaderboard',
  imports: [],
  templateUrl: './leaderboard.html',
  styleUrl: './leaderboard.scss',
})
export class Leaderboard implements OnInit {
  @Input() kids: Kid[] = [];

  leaderboardKids: Kid[] = [];
  rankClasses = ['gold', 'silver', 'bronze'];
  rankEmojis = ['🥇', '🥈', '🥉'];

  ngOnInit(): void {
    this.leaderboardKids = this.kids.sort((a, b) => b.totalPoints - a.totalPoints).slice(0, 3);
  }
}
