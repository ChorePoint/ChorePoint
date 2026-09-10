import {Component, effect, inject, signal, OnInit} from '@angular/core';
import {ShopForm} from '../../../../../shared/components/shop-form/shop-form';
import {ShopService} from '../../../../../core/services/shop/shop.service';
import {KidsService} from '../../../../../core/services/kids/kids.service';
import {ActivatedRoute} from '@angular/router';
import {Location} from '@angular/common';
import {createDefaultShopItemForm} from '../default-form';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-edit-shop-item',
  imports: [ShopForm],
  templateUrl: './edit-shop-item.html',
})
export class EditShopItem implements OnInit {
  private route = inject(ActivatedRoute);

  private shopService = inject(ShopService);
  private kidsService = inject(KidsService);

  loading = signal(false);
  error = signal<string | null>(null);
  kidsSignal = this.kidsService.kids;

  location = inject(Location);

  shopItemId!: number;
  form = createDefaultShopItemForm();

  constructor() {
    effect(() => {
      const shopItem = this.shopService.shopItems()
        .find((s) => s.shopItemId === this.shopItemId);

      if (!shopItem) return;

      this.form.patchValue({
        assignedKids: shopItem.assignedKids,
        icon: shopItem.icon,
        name: shopItem.name,
        description: shopItem.description,
        category: shopItem.category,
        cost: shopItem.cost,
        quantity: shopItem.quantity
      });
    });
  }

  ngOnInit() {
    this.shopItemId = Number(this.route.snapshot.paramMap.get('id'));
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading.set(true);

    const formData = { ...this.form.getRawValue(), shopItemId: this.shopItemId };

    this.shopService
      .updateShopItem$(formData)
      .pipe(
        finalize(() => {
          this.loading.set(false);
          this.form.reset();
        })
      )
      .subscribe({
        next: () => {
          this.location.back();
        },
        error: (err) => {
          this.error.set('Failed to update chore!');
          console.error(err);
        },
      });
  }
}
