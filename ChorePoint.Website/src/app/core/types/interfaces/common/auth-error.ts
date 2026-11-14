import { AuthErrorType } from '../../enums/common/auth-error-type';

export interface AuthError {
  type: AuthErrorType;
}
