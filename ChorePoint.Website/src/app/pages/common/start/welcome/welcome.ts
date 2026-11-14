import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-welcome',
  imports: [],
  templateUrl: './welcome.html',
  styleUrl: './welcome.scss',
})
export class Welcome {
  @Output() nextSlideEvent = new EventEmitter<void>();
  @Output() skipTutorialEvent = new EventEmitter<void>();

  nextSlide() {
    this.nextSlideEvent.emit();
  }

  skipTutorial() {
    this.skipTutorialEvent.emit();
  }
}
