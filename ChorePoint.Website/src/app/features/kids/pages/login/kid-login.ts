import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.service';
import { AuthError } from '../../../auth/models/auth.types';
import { getAuthErrorMessage } from '../../../auth/models/auth.error';

@Component({
  selector: 'app-parent-dashboard',
  imports: [RouterLink],
  templateUrl: './kid-login.html',
  styleUrl: './kid-login.scss',
})
export class KidLogin {
  private authService = inject(AuthService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  loading = signal(false);
  error = signal<string | null>(null);

  loginCode = '      ';

  submit() {
    this.loading.set(true);
    this.error.set(null);

    this.authService.kidLogin({ loginCode: this.loginCode }).subscribe({
      next: () => {
        const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || '/dashboard';
        this.router.navigateByUrl(returnUrl);
      },
      error: (err: AuthError) => {
        this.error.set(getAuthErrorMessage(err.type));
        this.loading.set(false);
      },
    });
  }

  onInput(e: Event, index: number) {
    const input = e.target as HTMLInputElement;

    const tempCode = this.loginCode.split('');

    tempCode[index] = input.value;
    this.loginCode = tempCode.join('');
  }
}
