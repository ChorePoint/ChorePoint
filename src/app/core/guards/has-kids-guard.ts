import { inject, Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { map, Observable, of } from 'rxjs';
import { KidsService } from '../services/kids/kids.service';

@Injectable({ providedIn: 'root' })
export class HasKidsGuard implements CanActivate {
  private kidsService = inject(KidsService);
  private router = inject(Router);

  canActivate(): Observable<boolean | UrlTree> {
    // Fast path — trust the flag
    if (localStorage.getItem('chorepoint_has_kids') === 'true') {
      return of(true);
    }

    // Slow path — verify with API
    return this.kidsService.getKids$().pipe(
      map((kids) => {
        if (kids.length > 0) {
          localStorage.setItem('chorepoint_has_kids', 'true');
          return true;
        }
        return this.router.createUrlTree(['/welcome']);
      }),
    );
  }
}
