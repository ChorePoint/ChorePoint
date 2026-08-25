import { TitleCasePipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Chore } from '../../../core/types/dtos/chore';
import { Kid } from '../../../core/types/dtos/kid';
import { LoadingAction } from '../../types/loading-action';
import { TimeFrame } from '../../types/timeframe';
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
  @Input() timeframe!: TimeFrame;
  @Input() loadingAction: LoadingAction | null = null;

  @Output() deleteEmitter = new EventEmitter<Chore>();
  @Output() toggleActiveEmitter = new EventEmitter<{ chore: Chore; active: boolean }>();

  toggleActive(chore: Chore) {
    this.toggleActiveEmitter.emit({ chore, active: this.getActive() > 0 });
  }

  delete(chore: Chore) {
    this.deleteEmitter.emit(chore);
  }

  getActive() {
    return this.chores.filter((chore) => chore.assignedKids.some((ak) => ak.isVisible)).length;
  }
}
