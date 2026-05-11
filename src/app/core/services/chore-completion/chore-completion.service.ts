import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { catchError } from 'rxjs/internal/operators/catchError';
import { KidStats } from './chore-completion.dtos';

@Injectable({ providedIn: 'root' })
export class ChoreCompletionService {
  private http = inject(HttpClient);

  private baseUrl = 'https://localhost:7087/api/chore';

  getChoreCompletionStats(kidId: number) {
    return this.http.get<KidStats>(`${this.baseUrl}/stats/${kidId}`).pipe(
      catchError((error) => {
        console.error('Error fetching chore completion stats:', error);
        throw error;
      }),
    );
  }
}
