import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, of, throwError } from 'rxjs';
import { catchError } from 'rxjs/internal/operators/catchError';
import { DEFAULT_KID_STATS } from '../../../consts/default-kid-stats';
import { ChoreSubmission } from '../../types/dtos/chore-submission';
import { ApiGetResponse, ApiPutResponse } from '../dtos/response';
import { KidStats } from './chore-submission.dtos';

@Injectable({ providedIn: 'root' })
export class ChoreSubmissionService {
  private http = inject(HttpClient);

  private baseUrl = 'https://localhost:7087/api/chore/submissions';

  getSubmissions$(pending: boolean) {
    return this.http
      .get<ApiGetResponse<ChoreSubmission[]>>(`${this.baseUrl}?pending=${pending}`)
      .pipe(
        map((res) => res.data),
        catchError((err) => (err.status === 404 ? of([]) : throwError(() => err))),
      );
  }

  getChoreSubmissionStats$(kidId: number) {
    return this.http.get<ApiGetResponse<KidStats>>(`${this.baseUrl}/stats/${kidId}`).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of(DEFAULT_KID_STATS) : throwError(() => err))),
    );
  }

  getCurrent$(userId: number) {
    return this.http.get<ApiGetResponse<ChoreSubmission>>(`${this.baseUrl}/current/${userId}`).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
    );
  }

  completeChore$(id: number) {
    return this.http.post(`${this.baseUrl}/${id}/complete`, {});
  }

  reviewChore$(submissionId: number, approve: boolean) {
    return this.http
      .put<ApiPutResponse>(`${this.baseUrl}/${submissionId}/review?approve=${approve}`, {})
      .pipe(
        map((res) => res),
        catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
      );
  }
}
