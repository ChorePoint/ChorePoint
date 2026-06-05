import { TitleCasePipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Chore } from '../../../core/types/dtos/chore';
import { Kid } from '../../../core/types/dtos/kid';
import { ChoreCard } from '../chore-card/chore-card';

@Component({
  selector: 'app-chore-card-wrapper',
  imports: [ChoreCard, TitleCasePipe],
  templateUrl: './chore-card-wrapper.html',
  styleUrl: './chore-card-wrapper.scss',
})
export class ChoreCardWrapper {
  @Input() chores!: Chore[];
  @Input() kidsDictionary!: Record<number, Kid>;
  @Input() timeframe!: 'daily' | 'weekly' | 'bonus';

  @Output() deleteEmitter = new EventEmitter<Chore>();
  @Output() toggleActiveEmitter = new EventEmitter<Chore>();

  toggleActive(chore: Chore) {
    this.toggleActiveEmitter.emit(chore);
  }

  delete(chore: Chore) {
    this.deleteEmitter.emit(chore);
  }

  getActive() {
    return this.chores.filter((chore) => chore.isVisible).length;
  }
}
