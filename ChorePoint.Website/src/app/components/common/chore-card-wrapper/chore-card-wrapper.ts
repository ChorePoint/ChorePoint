import { TitleCasePipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ChoreCard } from '../chore-card/chore-card';
import { Chore } from '../../../core/types/dtos/parent/chore';
import { Kid } from '../../../core/types/dtos/parent/kid';
import { TimeFrame } from '../../../core/types/enums/common/timeframe';
import { LoadingAction } from '../../../core/types/enums/common/loading-action';

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
