import { Component, Input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-form-field',
  imports: [ReactiveFormsModule],
  templateUrl: './form-field.html',
  styleUrl: './form-field.scss',
})
export class FormField {
  @Input() label!: string;
  @Input() icon!: string;
  @Input() type = 'text';
  @Input() placeholder = '';
  @Input() control!: FormControl;
}
