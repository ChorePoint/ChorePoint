import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { Observable, of } from 'rxjs';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs/internal/operators/catchError';
import { map } from 'rxjs/internal/operators/map';
import { KidIdentity } from '../../../types/dtos/kid/kid-identity';
import { DEFAULT_KID_IDENTITY } from '../../../consts/kid/default-kid-identity';
import { Kid } from '../../../types/dtos/parent/kid';
import { ApiGetResponse } from '../../common/dtos/response';

@Injectable({ providedIn: 'root' })
export class KidIdentityService {
  private http = inject(HttpClient);

  private baseUrl = '/api/kid';

  private _identity = signal<KidIdentity>(DEFAULT_KID_IDENTITY);
  readonly identity = this._identity.asReadonly();

  constructor() {
    this.refresh();
  }

  refresh() {
    this.getMe$().subscribe({
      next: (identity) => this._identity.set(identity),
      error: (err) => console.error('Failed to load kid profile', err),
    });
  }

  getKids$(): Observable<Kid[]> {
    return this.http.get<ApiGetResponse<Kid[]>>(`${this.baseUrl}/kids`).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of([]) : throwError(() => err))),
    );
  }

  getMe$(): Observable<KidIdentity> {
    return this.http.get<ApiGetResponse<KidIdentity>>(`${this.baseUrl}/me`).pipe(
      map((res) => res.data),
      catchError((err) => throwError(() => err)),
    );
  }
}
