import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Kid } from '../../../../core/types/dtos/kid';

@Component({
  selector: 'app-kid-selector-header',
  imports: [],
  templateUrl: './kid-selector-header.html',
  styleUrl: './kid-selector-header.scss',
})
export class KidSelectorHeader {
  @Input() kids!: Kid[];
  @Input() selectedKidId?: number;

  @Output() kidSelected = new EventEmitter<Kid>();

  onKidSelected(kid: Kid) {
    this.kidSelected.emit(kid);
  }
}
