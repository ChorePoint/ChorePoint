import { AuthErrorType } from './auth.types';

export const AUTH_ERROR_MAP: Record<number, AuthErrorType> = {
  401: AuthErrorType.InvalidCredentials,
  0: AuthErrorType.NetworkError,
};

export const AUTH_ERROR_MESSAGES: Record<AuthErrorType, string> = {
  [AuthErrorType.InvalidCredentials]: 'Invalid email or password.',
  [AuthErrorType.NetworkError]: 'Network error. Please check your connection.',
  [AuthErrorType.LoginFailed]: 'Login failed. Please try again.',
};

export function getAuthErrorMessage(type: AuthErrorType): string {
  return AUTH_ERROR_MESSAGES[type] ?? 'Something went wrong.';
}
