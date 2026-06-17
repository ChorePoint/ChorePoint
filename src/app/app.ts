import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PhoneFrame } from './layout/phone-frame/phone-frame';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, PhoneFrame],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  protected readonly title = signal('ChorePoint');
}
