import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { catchError } from 'rxjs/internal/operators/catchError';
import { map } from 'rxjs/internal/operators/map';
import { Chore } from '../../types/dtos/chore';
import { CreateChoreRequest, GetChoresResponse } from './chore.dtos';

@Injectable({ providedIn: 'root' })
export class ChoreService {
  private http = inject(HttpClient);

  private baseUrl = 'https://localhost:7087/api/chore';

  getById(id: number): Observable<Chore> {
    return this.http.get<Chore>(`${this.baseUrl}/${id}`).pipe(
      catchError((error) => {
        console.error('Error fetching chore details:', error);
        throw error;
      }),
      map((dto) => dto),
    );
  }

  getAll() {
    return this.http.get<Chore[]>(this.baseUrl);
  }

  createChore(request: CreateChoreRequest) {
    return this.http.post<void>(`${this.baseUrl}/create`, request);
  }

  getChores(visible = true) {
    return this.http
      .get<GetChoresResponse>(`${this.baseUrl}/user?visible=${visible}`)
      .pipe(
        catchError((error) => {
          console.error('Error fetching chores:', error);
          throw error;
        }),
      )
      .pipe(map((response) => response.data));
  }
}
