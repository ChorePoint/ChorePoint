import { Component, computed, inject, signal } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { ShopService } from '../../../../core/services/shop/shop.service';
import { Kid } from '../../../../core/types/dtos/kid';
import { ShopItem, ShopItemCard } from '../../../../core/types/dtos/shop-item';
import { Header } from '../../../../shared/components/header/header';
import { ShopCard } from '../../../../shared/components/shop-card/shop-card';
import { LoadingScreen } from '../../../../shared/pages/loading-screen/loading-screen';
import { KidSelectorHeader } from '../../../chores/components/kid-selector-header/kid-selector-header';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-shop',
  imports: [Header, LoadingScreen, KidSelectorHeader, ShopCard],
  templateUrl: './shop.html',
  styleUrl: './shop.scss',
})
export class Shop {
  private kidService = inject(KidsService);
  private shopService = inject(ShopService);

  private route = inject(ActivatedRoute);

  loading = true;
  deleteLoadingId = -1;

  toastState = {
    visible: false,
    text: 'Status updated!',
    success: true,
  };

  filteredShopItems = computed(() => {
    return this.vm
      .shopItems()
      .filter(
        (s) =>
          s.assignedKids.map((k) => k.kidId).includes(this.vm.selectedKid()?.kidId ?? -1) ||
          this.vm.selectedKid() == null,
      );
  })

  vm = {
    kids: this.kidService.kids,
    kidId: signal<number | null>(-1),
    selectedKid: computed(() => this.getSelectedKid()),
    shopItems: this.shopService.shopItems
  };

  constructor() {
    this.route.queryParamMap.subscribe(params => {
      const param = params.get('kidId');
      let kidId = null;

      if (param !== null) { kidId = Number(param) }

      this.vm.kidId.set(kidId);
    });
  }

  setKidId(kid: Kid | null) {
    this.vm.kidId.set(kid?.kidId ?? null);
  }

  getSelectedKid(): null | Kid {
    if (this.vm.kidId === null) return null;

    return this.kidService.kids().find(k => k.kidId == this.vm.kidId()) ?? null
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
}
