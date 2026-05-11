import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable, of, shareReplay, tap } from 'rxjs';
import { Kid } from '../../../features/kids/models/user';
import { UserService } from './kids.service';

@Injectable({ providedIn: 'root' })
export class KidsService {
  private userService = inject(UserService);

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

    this.loading$ = this.userService.getKids().pipe(
      tap((response) => {
        this.kidsSubject.next(response);
      }),
      shareReplay(1),
    );

    return this.loading$;
  }
}
