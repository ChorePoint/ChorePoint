import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { catchError, map, of, tap, throwError } from 'rxjs';
import { ShopItem } from '../../types/dtos/shop-item';
import { ApiGetResponse } from '../dtos/response';
import {NewShopItemRequest, UpdateShopItemRequest} from './shop.dtos';
import {UpdateChoreRequest} from '../chore/chore.dtos';

@Injectable({ providedIn: 'root' })
export class ShopService {
  private http = inject(HttpClient);

  private baseUrl = '/api/shop';

  private _shopItems = signal<ShopItem[]>([]);
  readonly shopItems = this._shopItems.asReadonly();

  constructor() {
    this.refresh();
  }

  refresh() {
    this.getShopItems().subscribe({
      next: (shopItems) => this._shopItems.set(shopItems),
      error: (err) => console.error('Failed to load shop items', err),
    });
  }

  newShopItem$(request: NewShopItemRequest) {
    return this.http.post<void>(`${this.baseUrl}/new`, request).pipe(tap(() => this.refresh()));
  }

  updateShopItem$(request: UpdateShopItemRequest) {
    return this.http.put<void>(`${this.baseUrl}/update`, request).pipe(
      tap(() => this.refresh()),
      map((res) => res),
      catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
    );
  }

  getShopItems() {
    return this.http.get<ApiGetResponse<ShopItem[]>>(`${this.baseUrl}/parent`).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of([]) : throwError(() => err))),
    );
  }

  deleteShopItem$(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/delete/${id}`).pipe(
      tap(() => this.refresh()),
      map((res) => res),
      catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
    );
  }
}
