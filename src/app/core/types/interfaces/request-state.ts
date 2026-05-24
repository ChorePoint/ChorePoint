export interface RequestState<T> {
  isLoading: boolean;
  data: T | null;
  message?: string;
  success?: boolean;
}
