import {Component, Input} from '@angular/core';
import {ToastState} from './types';

@Component({
  selector: 'app-toast-popup',
  imports: [],
  templateUrl: './toast-popup.html',
  styleUrl: './toast-popup.scss',
})
export class ToastPopup {
  @Input() toastState!: ToastState
}
