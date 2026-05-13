import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { of } from 'rxjs';
import { catchError } from 'rxjs/internal/operators/catchError';
import { ChoreSubmission } from '../../types/dtos/chore-submission';
import { DEFAULT_KID_STATS, KidStats } from './chore-submission.dtos';

@Injectable({ providedIn: 'root' })
export class ChoreSubmissionService {
  private http = inject(HttpClient);

  private baseUrl = 'https://localhost:7087/api/chore/submissions';

  getChoreSubmissionStats(kidId: number) {
    return this.http.get<KidStats>(`${this.baseUrl}/stats/${kidId}`).pipe(
      catchError((error) => {
        if (error.status === 404) {
          return of(DEFAULT_KID_STATS);
        } else {
          console.error('Error fetching chore submission stats:', error);
          throw error;
        }
      }),
    );
  }

  getCurrent(userId: number) {
    return this.http.get<ChoreSubmission>(`${this.baseUrl}/current/${userId}`).pipe(
      catchError((error) => {
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
