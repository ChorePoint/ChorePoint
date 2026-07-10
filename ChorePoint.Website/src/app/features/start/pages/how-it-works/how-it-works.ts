import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-how-it-works',
  imports: [],
  templateUrl: './how-it-works.html',
  styleUrl: './how-it-works.scss',
})
export class HowItWorks {
  @Output() nextSlideEvent = new EventEmitter<void>();
  @Output() prevSlideEvent = new EventEmitter<void>();

  nextSlide() {
    this.nextSlideEvent.emit();
  }

  prevSlide() {
    this.prevSlideEvent.emit();
  }
}
