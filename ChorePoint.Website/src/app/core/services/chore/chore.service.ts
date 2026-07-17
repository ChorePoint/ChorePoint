import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { of, tap } from 'rxjs';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs/internal/operators/catchError';
import { map } from 'rxjs/internal/operators/map';
import { Chore } from '../../types/dtos/chore';
import { ApiGetResponse } from '../dtos/response';
import { CreateChoreRequest, UpdateChoreRequest } from './chore.dtos';

@Injectable({ providedIn: 'root' })
export class ChoreService {
  private http = inject(HttpClient);

  private baseUrl = '/api/chore';

  private _chores = signal<Chore[]>([]);
  readonly chores$ = this._chores.asReadonly();

  constructor() {
    this.refresh();
  }

  refresh() {
    this.getChores$().subscribe({
      next: (chores) => this._chores.set(chores),
      error: (err) => console.error('Failed to load chores', err),
    });
  }

  getById$(id: number) {
    return this.http.get<ApiGetResponse<Chore>>(`${this.baseUrl}/${id}`).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
    );
  }

  getChores$(visible?: boolean) {
    let url = `${this.baseUrl}/parent`;
    if (visible !== undefined) {
      url += `?visible=${visible}`;
    }

    return this.http.get<ApiGetResponse<Chore[]>>(url).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of([]) : throwError(() => err))),
    );
  }

  createChore$(request: CreateChoreRequest) {
    return this.http.post<void>(`${this.baseUrl}/create`, request).pipe(tap(() => this.refresh()));
  }

  updateChore$(request: UpdateChoreRequest) {
    return this.http.put<void>(`${this.baseUrl}/update`, request).pipe(
      tap(() => this.refresh()),
      map((res) => res),
      catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
    );
  }

  deleteChore$(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/delete/${id}`).pipe(
      tap(() => this.refresh()),
      map((res) => res),
      catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
    );
  }
}
