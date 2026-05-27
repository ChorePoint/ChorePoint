import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, of } from 'rxjs';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs/internal/operators/catchError';
import { map } from 'rxjs/internal/operators/map';
import { Kid } from '../../types/dtos/kid';
import { ApiGetResponse } from '../dtos/response';

@Injectable({ providedIn: 'root' })
export class KidsService {
  private http = inject(HttpClient);

  private baseUrl = 'https://localhost:7087/api/parent';

  getKids$(): Observable<Kid[]> {
    return this.http.get<ApiGetResponse<Kid[]>>(`${this.baseUrl}/kids`).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of([]) : throwError(() => err))),
    );
  }
}
