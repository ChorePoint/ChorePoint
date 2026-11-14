import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-form-header',
  imports: [],
  templateUrl: './form-header.html',
  styleUrl: './form-header.scss',
})
export class FormHeader {
  @Input() subtitle!: string;
  @Input() logo!: string;
  @Input() title!: string;
  @Input() tagline!: string;
}
