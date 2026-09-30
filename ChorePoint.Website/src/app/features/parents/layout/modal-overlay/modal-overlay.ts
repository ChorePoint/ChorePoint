import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-modal-overlay',
  imports: [],
  templateUrl: './modal-overlay.html',
  styleUrl: './modal-overlay.scss',
})
export class ModalOverlay {
  @Output() changeLoginModalVisibility = new EventEmitter<boolean>();

  closeLoginCodeModal() {
    this.changeLoginModalVisibility.emit(false);
  }
}
