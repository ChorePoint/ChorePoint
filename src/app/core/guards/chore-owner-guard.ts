import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class ChoreOwnerGuard implements CanActivate {
  private router = inject(Router);

  async canActivate(route: ActivatedRouteSnapshot): Promise<boolean> {
    return true;
  }
}
