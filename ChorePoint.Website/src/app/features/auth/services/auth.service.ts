import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { catchError, tap, throwError } from 'rxjs';
import { AUTH_ERROR_MAP } from '../models/auth.error';
import { AuthError, AuthErrorType } from '../models/auth.types';
import { RegisterRequest } from '../models/create-account-request';
import { LoginRequest, LoginResponse } from '../models/login-request';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);

  private baseUrl = 'api/auth';

  private ONBOARDING_KEY = 'hasSeenOnboarding';

  isTokenExpired(token: string): boolean {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.exp * 1000 < Date.now();
    } catch {
      return true; // Invalid token is considered expired
    }
  }

  isAuthenticated(): boolean {
    const authToken = localStorage.getItem('authToken');

    if (!authToken) return false;

    const isExpired = this.isTokenExpired(authToken);

    if (isExpired) {
      localStorage.removeItem('authToken');
      return false;
    }

    return true;
  }

  hasSeenOnboarding(): boolean {
    return localStorage.getItem(this.ONBOARDING_KEY) === 'true';
  }

  setOnboardingSeen(): void {
    localStorage.setItem(this.ONBOARDING_KEY, 'true');
  }

  login(request: LoginRequest) {
    return this.http.post<LoginResponse>(`${this.baseUrl}/login`, request).pipe(
      tap((response) => {
        localStorage.setItem('authToken', response.data.token);
      }),
      catchError((err: HttpErrorResponse) => {
        const type = AUTH_ERROR_MAP[err.status] ?? AuthErrorType.LoginFailed;
        return throwError(() => ({ type }) as AuthError);
      }),
    );
  }

  logout() {
    localStorage.removeItem('authToken');
  }

  createAccount(request: RegisterRequest) {
    return this.http.post<void>(`${this.baseUrl}/register`, request);
  }
}
