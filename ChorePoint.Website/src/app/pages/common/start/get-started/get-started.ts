import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-get-started',
  imports: [],
  templateUrl: './get-started.html',
  styleUrl: './get-started.scss',
})
export class GetStarted {
  @Output() nextSlideEvent = new EventEmitter<void>();

  nextSlide() {
    this.nextSlideEvent.emit();
  }
}
