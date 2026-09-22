export interface LoadingAction {
  choreId: number | null;
  type: LoadingType
}

export enum LoadingType {
  Activate = 'activate',
  Deactivate = 'deactivate',
  Delete = 'delete',
}
