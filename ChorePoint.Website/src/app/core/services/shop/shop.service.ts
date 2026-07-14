import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { catchError, map, of, throwError } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { ShopItem } from '../../types/dtos/shop-item';
import { ApiGetResponse } from '../dtos/response';
import { NewShopItemRequest } from './shop.dtos';

@Injectable({ providedIn: 'root' })
export class ShopService {
  private http = inject(HttpClient);

  private baseUrl = `${environment.apiUrl}/api/shop`;

  newShopItem$(request: NewShopItemRequest) {
    return this.http.post<void>(`${this.baseUrl}/new`, request);
  }

  getShopItems$() {
    return this.http.get<ApiGetResponse<ShopItem[]>>(`${this.baseUrl}/parent`).pipe(
      map((res) => res.data),
      catchError((err) => (err.status === 404 ? of([]) : throwError(() => err))),
    );
  }

  deleteShopItem$(id: number) {
    return this.http.delete<void>(`${this.baseUrl}/${id}/delete`).pipe(
      map((res) => res),
      catchError((err) => (err.status === 404 ? of(null) : throwError(() => err))),
    );
  }
}
