import { Component, computed, inject } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { ShopService } from '../../../../core/services/shop/shop.service';
import { Kid } from '../../../../core/types/dtos/kid';
import { ShopItem, ShopItemCard } from '../../../../core/types/dtos/shop-item';
import { SHOP_ITEM_STATUS_MAP } from '../../../../core/types/enums/shop-item-status';
import { Header } from '../../../../shared/components/header/header';
import { ShopCard } from '../../../../shared/components/shop-card/shop-card';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { KidSelectorHeader } from '../../../chores/components/kid-selector-header/kid-selector-header';

@Component({
  selector: 'app-shop',
  imports: [Header, LoadingScreen, KidSelectorHeader, ShopCard],
  templateUrl: './shop.html',
  styleUrl: './shop.scss',
})
export class Shop {
  private shopService = inject(ShopService);
  private kidsService = inject(KidsService);

  SHOP_ITEM_STATUS_MAP = SHOP_ITEM_STATUS_MAP;
  loading = true;
  deleteLoadingId = -1;

  vm = {
    selectedKid: (this.kidsService.kids().at(0) ?? null) as Kid | null,
    kids: this.kidsService.kids,
    shopItems: this.shopService.shopItems,
    filteredShopItems: computed(() => this.getFilteredShopItems()),
  };

  delete(id: number) {
    this.deleteLoadingId = id;

    this.shopService
      .deleteShopItem$(id)
      .pipe(
        finalize(() => {
          this.deleteLoadingId = -1;
        }),
      )
      .subscribe();
  }

  getAssignedKidsNames(shopItem: ShopItem) {
    const kidIds = shopItem.assignedKids.map((s) => s.kidId);

    let names = '👤 All Kids';
    if (this.vm.kids().length !== kidIds.length) {
      names = [
        ...new Set(
          this.vm
            .kids()
            .filter((k) => kidIds.includes(k.kidId))
            .map((k) => k.name),
        ),
      ].join(', ');
    }

    return names;
  }

  getShopItemCard(shopItem: ShopItem): ShopItemCard {
    const kidNames = this.getAssignedKidsNames(shopItem);

    return {
      ...shopItem,
      assignedKidsString: kidNames,
    };
  }

  getFilteredShopItems(): ShopItem[] {
    console.log('HERE');
    return this.vm
      .shopItems()
      .filter(
        (s) =>
          s.assignedKids.map((k) => k.kidId).includes(this.vm.selectedKid?.kidId ?? -1) ||
          this.vm.selectedKid == null,
      );
  }
}
