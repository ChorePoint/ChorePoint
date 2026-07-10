export interface LoadingAction {
  choreId: number | null;
  type: 'activate' | 'delete';
}

export enum LoadingType {
  Activate = 'activate',
  Delete = 'delete',
}
