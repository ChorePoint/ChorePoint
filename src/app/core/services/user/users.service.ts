import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { catchError } from 'rxjs/internal/operators/catchError';
import { map } from 'rxjs/internal/operators/map';
import { GetKidsResponse } from './get-kids.response';

@Injectable({ providedIn: 'root' })
export class UserService {
  private http = inject(HttpClient);

  private baseUrl = 'https://localhost:7087/api/user';

  getKids() {
    return this.http
      .get<GetKidsResponse>(`${this.baseUrl}/kids`)
      .pipe(
        catchError((error) => {
          console.error('Error fetching children:', error);
          throw error;
        }),
      )
      .pipe(
        // Map the response to return just the array of kids
        map((response) => response.data),
      );
  }
}
