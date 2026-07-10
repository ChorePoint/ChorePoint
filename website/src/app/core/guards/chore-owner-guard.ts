import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class ChoreOwnerGuard implements CanActivate {
  async canActivate(): Promise<boolean> {
    return true;
  }
}
