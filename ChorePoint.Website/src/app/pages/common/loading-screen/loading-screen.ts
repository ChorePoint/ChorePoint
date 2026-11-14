import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-loading-screen',
  imports: [FontAwesomeModule],
  templateUrl: './loading-screen.html',
  styleUrl: './loading-screen.scss',
})
export class LoadingScreen {
  back = faArrowLeft;

  onBackClick() {
    window.history.back();
  }
}
