import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormField } from '../../../components/common/form-field/form-field';
import { PasswordInput } from '../../../components/common/password-input/password-input';
import { LoadingEmoji } from '../../../components/common/loading-emoji/loading-emoji';
import { AuthService } from '../../../core/services/common/auth/auth.service';
import { AuthError } from '../../../core/types/interfaces/common/auth-error';
import { getAuthErrorMessage } from '../../../core/consts/common/auth-error';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, LoadingEmoji, RouterModule, PasswordInput, FormField],
  templateUrl: './parent-login.component.html',
  styleUrl: './parent-login.component.scss',
})
export class ParentLogin {
  private authService = inject(AuthService);
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  loading = signal(false);
  error = signal<string | null>(null);

  form = this.fb.nonNullable.group({
    email: ['', { validators: [Validators.required, Validators.email], updateOn: 'blur' }],
    password: ['', { validators: [Validators.required], updateOn: 'blur' }],
  });

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    const { email, password } = this.form.getRawValue();

    this.authService.login({ email, password }).subscribe({
      next: () => {
        const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || '/parent';
        this.router.navigateByUrl(returnUrl);
      },
      error: (err: AuthError) => {
        this.error.set(getAuthErrorMessage(err.type));
        this.loading.set(false);
      },
    });
  }
}
