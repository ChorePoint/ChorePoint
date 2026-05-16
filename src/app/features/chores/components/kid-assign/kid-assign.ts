import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Kid } from '../../../../core/types/dtos/kid';

@Component({
  selector: 'app-kid-assign',
  imports: [],
  templateUrl: './kid-assign.html',
  styleUrl: './kid-assign.scss',
})
export class KidAssign {
  @Input() selectedKid: number | undefined;
  @Input() kids!: Kid[];

  @Output() selectedKidChange = new EventEmitter<number>();
}
