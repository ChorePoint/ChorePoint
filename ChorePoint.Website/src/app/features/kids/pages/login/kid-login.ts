import { Component, ElementRef, inject, signal, viewChildren } from '@angular/core';
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

  codeInputs = viewChildren<ElementRef<HTMLInputElement>>('codeInput');

  loading = signal(false);
  error = signal<string | null>(null);

  loginCodeEntries = ['', '', '-', '', '', '-', '', ''];
  loginCode = '';

  submit() {
    if (this.loginCode.length != 8) return;

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

  onBackspace(e: Event, index: number) {
    if (index == 0) return;

    if (index == 5 && this.codeInputs()[index].nativeElement.value != '') {
      index += 1;
    }

    e.preventDefault();

    this.codeInputs()[index - 1].nativeElement.focus();
    this.codeInputs()[index - 1].nativeElement.value = '';
  }

  onInput(e: Event, index: number) {
    const input = e.target as HTMLInputElement;

    if (index < 5) {
      this.codeInputs()[index + 1].nativeElement.focus();
    }

    // Accounting for the dashes in the login code
    if (index == 2 || index == 3) {
      index++;
    } else if (index == 4 || index == 5) {
      index += 2;
    }

    this.loginCodeEntries[index] = input.value;
    this.loginCode = this.loginCodeEntries.join('');
  }
}
