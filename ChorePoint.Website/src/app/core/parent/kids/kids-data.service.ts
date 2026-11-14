import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable, of, shareReplay, tap } from 'rxjs';
import { Kid } from '../../types/dtos/kid';
import { KidsService } from './kids.service';

@Injectable({ providedIn: 'root' })
export class KidsDataService {
  private kidsService = inject(KidsService);

  private kidsSubject = new BehaviorSubject<Kid[] | null>(null);
  kids$ = this.kidsSubject.asObservable();

  private loading$: Observable<Kid[]> | null = null;

  getKids$(): Observable<Kid[]> {
    if (this.kidsSubject.value) {
      return of(this.kidsSubject.value);
    }

    if (this.loading$) {
      return this.loading$;
    }

    this.loading$ = this.kidsService.getKids$().pipe(
      tap((response) => {
        this.kidsSubject.next(response);
      }),
      shareReplay(1),
    );

    return this.loading$;
  }
}
