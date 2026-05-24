import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { map, of, startWith } from 'rxjs';
import { catchError } from 'rxjs/internal/operators/catchError';
import { DEFAULT_KID_STATS } from '../../../consts/default-kid-stats';
import { ChoreSubmission } from '../../types/dtos/chore-submission';
import { RequestState } from '../../types/interfaces/request-state';
import { ApiResponse } from '../dtos/response';
import { GetKidStatsResponse } from './chore-submission.dtos';

@Injectable({ providedIn: 'root' })
export class ChoreSubmissionService {
  private http = inject(HttpClient);

  private baseUrl = 'https://localhost:7087/api/chore/submissions';

  getSubmissions$(pending: boolean) {
    return this.http.get<ApiResponse<ChoreSubmission[]>>(`${this.baseUrl}?pending=${pending}`).pipe(
      map(
        (res) =>
          ({
            isLoading: false,
            data: res.data,
            message: res.message,
            success: res.success,
          }) as RequestState<ChoreSubmission[]>,
      ),

      catchError((err) => {
        if (err.status === 404) {
          return of({
            isLoading: false,
            data: [],
            message: 'No submissions found',
            success: true,
          } satisfies RequestState<ChoreSubmission[]>);
        }

        throw err;
      }),

      startWith({
        isLoading: true,
        data: null,
      } as RequestState<ChoreSubmission[]>),
    );
  }

  getChoreSubmissionStats(kidId: number) {
    return this.http.get<GetKidStatsResponse>(`${this.baseUrl}/stats/${kidId}`).pipe(
      catchError((error) => {
        if (error.status === 404) {
          return of({
            success: error.success,
            message: error.message,
            data: DEFAULT_KID_STATS,
            isLoading: false,
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

  reviewChore(submissionId: number, approve: boolean) {
    return this.http
      .put<ApiResponse<string>>(`${this.baseUrl}/${submissionId}/review?approve=${approve}`, {})
      .pipe(
        catchError((error) => {
          console.error('Error approving chore:', error);
          throw error;
        }),
        map((response) => response.data),
      );
  }
}
