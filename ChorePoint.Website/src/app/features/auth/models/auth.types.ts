export enum AuthErrorType {
  InvalidCredentials = 'INVALID_CREDENTIALS',
  NetworkError = 'NETWORK_ERROR',
  LoginFailed = 'LOGIN_FAILED',
}

export interface AuthError {
  type: AuthErrorType;
}
