import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { of } from 'rxjs';
import { catchError } from 'rxjs/internal/operators/catchError';
import { ChoreSubmission } from '../../models/chore-submission';

@Injectable({ providedIn: 'root' })
export class ChoreSubmissionService {
  private http = inject(HttpClient);

  private baseUrl = 'https://localhost:7087/api/chore';

  getCurrent(userId: number) {
    return this.http.get<ChoreSubmission>(`${this.baseUrl}/current/${userId}`).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 404) {
          return of(null);
        }
        throw error;
      }),
    );
  }

  completeChore(id: number) {
    return this.http.post(`${this.baseUrl}/${id}/complete`, {});
  }
}
