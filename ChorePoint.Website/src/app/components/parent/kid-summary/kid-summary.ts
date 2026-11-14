import { Component, Input } from '@angular/core';
import { IKidsSummary } from './types';

@Component({
  selector: 'app-kid-summary',
  imports: [],
  templateUrl: './kid-summary.html',
  styleUrl: './kid-summary.scss',
})
export class KidSummary {
  @Input() summary!: IKidsSummary;
}
