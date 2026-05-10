import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable, of, shareReplay, tap } from 'rxjs';
import { User } from '../../features/kids/models/user';
import { UserService } from './user/users.service';

@Injectable({ providedIn: 'root' })
export class KidsService {
  private userService = inject(UserService);

  private kidsSubject = new BehaviorSubject<User[] | null>(null);
  kids$ = this.kidsSubject.asObservable();

  private loading$: Observable<User[]> | null = null;

  getKids$(): Observable<User[]> {
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
