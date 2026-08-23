import { HttpClient } from '@angular/common/http';
import { computed, inject, Injectable, signal } from '@angular/core';
import { map, of, tap, throwError } from 'rxjs';
import { catchError } from 'rxjs/internal/operators/catchError';
import { DEFAULT_KID_STATS } from '../../consts/default-kid-stats';
import { ChoreSubmission } from '../../types/dtos/chore-submission';
import { ApiGetResponse, ApiPutResponse } from '../dtos/response';
import { KidStats } from './chore-submission.dtos';

@Injectable({ providedIn: 'root' })
export class ChoreSubmissionService {
  private http = inject(HttpClient);

  private baseUrl = '/api/chore/submissions';

  private _submissions = signal<ChoreSubmission[]>([]);
  readonly submissions = this._submissions.asReadonly();

  private _submissionStats = signal(new Map<number, KidStats>());
  readonly submissionStats = this._submissionStats.asReadonly();

  constructor() {
    this.refresh();
  }

  refresh() {
    this.getSubmissions$(false).subscribe({
      next: (submissions) => this._submissions.set(submissions),
      error: (err) => console.error('Failed to load chore submissions', err),
    });
  }

  getSubmissions$(pending: boolean) {
    return this.http
      .get<ApiGetResponse<ChoreSubmission[]>>(`${this.baseUrl}?pending=${pending}`)
      .pipe(
        map((res) => res.data),
        catchError((err) => (err.status === 404 ? of([]) : throwError(() => err))),
      );
  }

  removeChildFromCache(kidId: number) {
    const newCache = new Map(this._submissionStats());
    newCache.delete(kidId);
    this._submissionStats.set(newCache);
  }

  loadChoreSubmissionStats(kidId: number) {
    if (this._submissionStats().get(kidId)) {
      return;
    }

    this.http
      .get<ApiGetResponse<KidStats>>(`${this.baseUrl}/stats/${kidId}`)
      .pipe(
        map((res) => res.data),
        catchError((err) => (err.status === 404 ? of(DEFAULT_KID_STATS) : throwError(() => err))),
      )
      .subscribe((stats) => {
        this._submissionStats.update((current) => {
          const map = new Map(current);
          map.set(kidId, stats);
          return map;
        });
      });
  }

  getChoreSubmissionStats(kidId: number) {
    return computed(() => this._submissionStats().get(kidId));
  }

  getCurrent$(userId: number) {
    return this.http.get<ApiGetResponse<ChoreSubmission>>(`${this.baseUrl}/current/${userId}`).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
    );
  }

  completeChore$(id: number) {
    return this.http.post(`${this.baseUrl}/${id}/complete`, {}).pipe(tap(() => this.refresh()));
  }

  reviewChore$(submissionId: number, approve: boolean) {
    return this.http
      .put<ApiPutResponse>(`${this.baseUrl}/${submissionId}/review?approve=${approve}`, {})
      .pipe(
        tap(() => this.refresh()),
        map((res) => res),
        catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
      );
  }
}
