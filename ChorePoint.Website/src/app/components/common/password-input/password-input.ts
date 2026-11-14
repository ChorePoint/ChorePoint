import { Component, Input, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-password-input',
  imports: [ReactiveFormsModule],
  templateUrl: './password-input.html',
  styleUrl: './password-input.scss',
})
export class PasswordInput implements OnInit {
  @Input() label!: string;
  @Input() icon!: string;
  @Input() placeholder = '';
  @Input() control!: FormControl;
  @Input() showStrengthBar = false;

  showPassword = false;

  password = '';
  strengthText = '';
  strength = 0;

  ngOnInit() {
    this.control.valueChanges.subscribe((value) => {
      this.calculateStrength(value || '');
    });
  }

  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }

  calculateStrength(password: string) {
    let score = 0;

    if (!password) {
      this.strength = 0;
      this.strengthText = '';
      return;
    }

    // Length check
    if (password.length >= 8) score++;

    // Contains number
    if (/\d/.test(password)) score++;

    // Contains uppercase
    if (/[A-Z]/.test(password)) score++;

    // Contains special char
    if (/[^A-Za-z0-9]/.test(password)) score++;

    this.strength = score;

    // Label
    if (score <= 1) this.strengthText = 'Weak';
    else if (score === 2) this.strengthText = 'Fair';
    else if (score === 3) this.strengthText = 'Good';
    else this.strengthText = 'Strong';
  }
}
