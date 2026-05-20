import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { map, of } from 'rxjs';
import { catchError } from 'rxjs/internal/operators/catchError';
import { ChoreSubmission } from '../../types/dtos/chore-submission';
import { ApiResponse } from '../dtos/response';
import {
  DEFAULT_KID_STATS,
  GetChoreSubmissionsResponse,
  GetKidStatsResponse,
  KidStats,
} from './chore-submission.dtos';

@Injectable({ providedIn: 'root' })
export class ChoreSubmissionService {
  private http = inject(HttpClient);

  private baseUrl = 'https://localhost:7087/api/chore/submissions';

  getSubmissions$() {
    return this.http.get<GetChoreSubmissionsResponse>(`${this.baseUrl}?pending=false`).pipe(
      catchError((error) => {
        if (error.status === 404) {
          return of({
            success: true,
            message: 'No chore submissions found',
            data: [],
          } satisfies GetChoreSubmissionsResponse);
        } else {
          console.error('Error fetching chore submissions:', error);
          throw error;
        }
      }),
      map((response) => response.data),
    );
  }

  getChoreSubmissionStats(kidId: number) {
    return this.http.get<GetKidStatsResponse>(`${this.baseUrl}/stats/${kidId}`).pipe(
      catchError((error) => {
        if (error.status === 404) {
          return of({
            success: true,
            message: 'No stats found',
            data: DEFAULT_KID_STATS as KidStats,
          } satisfies GetKidStatsResponse);
        } else {
          console.error('Error fetching chore submission stats:', error);
          throw error;
        }
      }),
      map((response) => response.data),
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

  reviewChore(submissionId: number, approve = true) {
    return this.http
      .post<ApiResponse<string>>(`${this.baseUrl}/${submissionId}/review?approve=${approve}`, {})
      .pipe(
        catchError((error) => {
          console.error('Error approving chore:', error);
          throw error;
        }),
        map((response) => response.data),
      );
  }
}
