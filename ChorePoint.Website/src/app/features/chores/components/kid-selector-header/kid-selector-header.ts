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
  @Input() kidHeaderText!: string;

  @Output() kidSelected = new EventEmitter<Kid | null>();

  onKidSelected(kid: Kid | null) {
    this.kidSelected.emit(kid);
  }
}
