import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignedKidToChore } from '../../../../core/types/dtos/assigned-kid-to-chore';
import { Kid } from '../../../../core/types/dtos/kid';

@Component({
  selector: 'app-kid-assign',
  imports: [],
  templateUrl: './kid-assign.html',
  styleUrl: './kid-assign.scss',
})
export class KidAssign {
  @Input() selectedKids: AssignedKidToChore[] | undefined;
  @Input() kids!: Kid[];

  @Output() selectedKidChange = new EventEmitter<number>();

  select(kidId: number) {
    this.selectedKidChange.emit(kidId);
  }

  isSelected(kidId: number) {
    return (
      this.selectedKids?.map((selectedKid) => selectedKid.kidId).includes(kidId) &&
      this.selectedKids.length === 1
    );
  }
}
