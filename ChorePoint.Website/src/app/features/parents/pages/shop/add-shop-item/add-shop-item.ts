import { Location } from '@angular/common';
import {Component, inject, OnInit, signal} from '@angular/core';
import {ShopForm} from '../../../../../shared/components/shop-form/shop-form';
import {ShopService} from '../../../../../core/services/shop/shop.service';
import {KidsService} from '../../../../../core/services/kids/kids.service';
import { ShopItemStatusStatus } from '../../../../../core/types/enums/shop-item-status';
import {createDefaultShopItemForm} from '../default-form';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-add-shop-item',
  imports: [ShopForm],
  templateUrl: './add-shop-item.html',
})
export class AddShopItem implements OnInit {

  private shopService = inject(ShopService);
  private kidsService = inject(KidsService);

  loading = signal(false);
  error = signal<string | null>(null);
  kidsSignal = this.kidsService.kids;

  location = inject(Location);

  form = createDefaultShopItemForm();

  ngOnInit() {
    if (this.kidsSignal().length !== 0 && !this.form.value.assignedKids) {
      this.form.patchValue({
        assignedKids: [
          {
            kidId: this.kidsSignal()[0].kidId,
            status: ShopItemStatusStatus.Available,
          },
        ],
      });
    }
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading.set(true);

    const formData = this.form.getRawValue();

    this.shopService
      .newShopItem$(formData)
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
          this.error.set('Failed to create chore!');
          console.error(err);
        },
      });
  }
}
