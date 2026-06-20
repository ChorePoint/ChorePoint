import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { NewShopItemRequest } from './shop.dtos';

@Injectable({ providedIn: 'root' })
export class ShopService {
  private http = inject(HttpClient);

  private baseUrl = 'https://localhost:7087/api/shop';

  newShopItem$(request: NewShopItemRequest) {
    return this.http.post<void>(`${this.baseUrl}/new`, request);
  }
}
