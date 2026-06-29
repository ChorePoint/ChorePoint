import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { BehaviorSubject, combineLatest, map, Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { KidsDataService } from '../../../../core/services/kids/kids-data.service';
import { ShopService } from '../../../../core/services/shop/shop.service';
import { Kid } from '../../../../core/types/dtos/kid';
import { ShopItem } from '../../../../core/types/dtos/shop-item';
import { SHOP_ITEM_STATUS_MAP } from '../../../../core/types/enums/shop-item-status';
import { Header } from '../../../../shared/components/header/header';
import { ShopCard } from '../../../../shared/components/shop-card/shop-card';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { KidSelectorHeader } from '../../../chores/components/kid-selector-header/kid-selector-header';

@Component({
  selector: 'app-shop',
  imports: [AsyncPipe, Header, LoadingScreen, KidSelectorHeader, ShopCard],
  templateUrl: './shop.html',
  styleUrl: './shop.scss',
})
export class Shop implements OnInit {
  private shopService = inject(ShopService);
  private kidsDataService = inject(KidsDataService);

  SHOP_ITEM_STATUS_MAP = SHOP_ITEM_STATUS_MAP;

  loading = true;
  deleteLoadingId = -1;

  private shopItemsSubject = new BehaviorSubject<ShopItem[]>([]);

  vm$!: Observable<{
    kids: Kid[];
    selectedKid: Kid | null;
    shopItems: ShopItem[];
  }>;

  ngOnInit() {
    this.shopService.getShopItems$().subscribe((items) => {
      this.shopItemsSubject.next(items);
      this.loading = false;
    });

    this.vm$ = combineLatest([
      this.kidsDataService.getKids$(),
      this.shopItemsSubject.asObservable(),
    ]).pipe(
      map(([kids, shopItems]) => ({
        kids: kids ?? [],
        selectedKid: kids?.[0] ?? null,
        shopItems,
      })),
    );
  }

  delete(id: number) {
    this.deleteLoadingId = id;

    this.shopService
      .deleteShopItem$(id)
      .pipe(
        finalize(() => {
          this.deleteLoadingId = -1;
        }),
      )
      .subscribe(() => {
        const currentItems = this.shopItemsSubject.value;

        this.shopItemsSubject.next(currentItems.filter((item) => item.id !== id));
      });
  }
}
