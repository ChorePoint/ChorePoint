export interface ApiGetResponse<T> {
  success: boolean;
  message: string;
  data: T;
}

export interface ApiPutResponse {
  success: boolean;
  message: string;
}
