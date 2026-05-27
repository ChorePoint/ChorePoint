import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { of } from 'rxjs';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs/internal/operators/catchError';
import { map } from 'rxjs/internal/operators/map';
import { Chore } from '../../types/dtos/chore';
import { ApiGetResponse } from '../dtos/response';
import { CreateChoreRequest } from './chore.dtos';

@Injectable({ providedIn: 'root' })
export class ChoreService {
  private http = inject(HttpClient);

  private baseUrl = 'https://localhost:7087/api/chore';

  getById$(id: number) {
    return this.http.get<ApiGetResponse<Chore>>(`${this.baseUrl}/${id}`).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
    );
  }

  getAll$() {
    return this.http.get<ApiGetResponse<Chore[]>>(this.baseUrl).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of([]) : throwError(() => err))),
    );
  }

  getChores$(visible = true) {
    return this.http.get<ApiGetResponse<Chore[]>>(`${this.baseUrl}/parent?visible=${visible}`).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of([]) : throwError(() => err))),
    );
  }

  createChore$(request: CreateChoreRequest) {
    return this.http.post<void>(`${this.baseUrl}/create`, request);
  }
}
