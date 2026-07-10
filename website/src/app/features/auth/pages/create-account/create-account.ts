import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FormField } from '../../../../shared/components/form-field/form-field';
import { FormHeader } from '../../../../shared/components/form-header/form-header';
import { LoadingEmoji } from '../../../../shared/components/loading-emoji/loading-emoji';
import { PasswordInput } from '../../../../shared/components/password-input/password-input';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-create-account',
  imports: [ReactiveFormsModule, FormField, FormHeader, PasswordInput, LoadingEmoji],
  templateUrl: './create-account.html',
  styleUrl: './create-account.scss',
})
export class CreateAccount {
  private authService = inject(AuthService);
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  loading = signal(false);
  error = signal<string | null>(null);

  form = this.fb.nonNullable.group({
    firstName: ['', { validators: [Validators.required], updateOn: 'blur' }],
    lastName: ['', { validators: [Validators.required], updateOn: 'blur' }],
    email: ['', { validators: [Validators.required, Validators.email], updateOn: 'blur' }],
    password: ['', { validators: [Validators.required] }],
    terms: [false, Validators.requiredTrue],
  });

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    const { firstName, lastName, email, password } = this.form.getRawValue();

    this.authService.createAccount({ firstName, lastName, email, password }).subscribe({
      next: () => {
        const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || '/login';
        this.router.navigateByUrl(returnUrl);
      },
      error: (err: HttpErrorResponse) => {
        if (err.status === 401) {
          this.error.set('Invalid email or password.');
        } else {
          this.error.set(err.message || 'Login failed. Please try again.');
        }
        this.loading.set(false);
      },
    });
  }
}
