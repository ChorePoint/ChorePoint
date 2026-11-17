import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { of } from 'rxjs';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs/internal/operators/catchError';
import { map } from 'rxjs/internal/operators/map';
import { Chore } from '../../../types/dtos/parent/chore';
import { ApiGetResponse } from '../dtos/response';

@Injectable({ providedIn: 'root' })
export class CommonChoreService {
  private http = inject(HttpClient);

  private baseUrl = '/api/chore';

  private _chores = signal<Chore[]>([]);
  readonly chores = this._chores.asReadonly();

  constructor() {
    this.refresh();
  }

  refresh() {
    this.getByKid$().subscribe({
      next: (chores) => this._chores.set(chores),
      error: (err) => console.error('Failed to load chores', err),
    });
  }

  getByKid$() {
    return this.http.get<ApiGetResponse<Chore[]>>(`${this.baseUrl}/kid`).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of([]) : throwError(() => err))),
    );
  }
}
