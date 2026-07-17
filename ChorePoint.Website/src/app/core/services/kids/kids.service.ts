import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { Observable, of, tap } from 'rxjs';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs/internal/operators/catchError';
import { map } from 'rxjs/internal/operators/map';
import { Kid } from '../../types/dtos/kid';
import { ApiGetResponse } from '../dtos/response';
import { CreateKidRequest, UpdateKidRequest } from './kid.dtos';

@Injectable({ providedIn: 'root' })
export class KidsService {
  private http = inject(HttpClient);

  private baseUrl = '/api/parent';

  private _kids = signal<Kid[]>([]);
  readonly kids$ = this._kids.asReadonly();

  constructor() {
    this.refresh();
  }

  refresh() {
    this.getKids$().subscribe({
      next: (kids) => this._kids.set(kids),
      error: (err) => console.error('Failed to load kids', err),
    });
  }

  getKids$(): Observable<Kid[]> {
    return this.http.get<ApiGetResponse<Kid[]>>(`${this.baseUrl}/kids`).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of([]) : throwError(() => err))),
    );
  }

  getKidById$(kidId: number): Observable<Kid> {
    return this.http.get<ApiGetResponse<Kid>>(`${this.baseUrl}/kid/${kidId}`).pipe(
      map((res) => res.data),
      catchError((err) => throwError(() => err)),
    );
  }

  createKid$(createKidRequest: CreateKidRequest) {
    return this.http.post<void>(`${this.baseUrl}/kid/create`, createKidRequest).pipe(
      tap(() => this.refresh()),
      map((res) => res),
      catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
    );
  }

  updateKid$(updateKidRequest: UpdateKidRequest) {
    return this.http.put<void>(`${this.baseUrl}/kid/update`, updateKidRequest).pipe(
      tap(() => this.refresh()),
      map((res) => res),
      catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
    );
  }

  deleteKidById$(kidId: number) {
    return this.http.delete<void>(`${this.baseUrl}/kid/delete/${kidId}`).pipe(
      tap(() => this.refresh()),
      map((res) => res),
      catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
    );
  }
}
