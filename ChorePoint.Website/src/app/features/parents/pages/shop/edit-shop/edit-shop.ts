import {Location} from '@angular/common';
import {Component, effect, inject, signal} from '@angular/core';
import {ShopForm} from '../../../../../shared/components/shop-form/shop-form';
import {ShopService} from '../../../../../core/services/shop/shop.service';
import {KidsService} from '../../../../../core/services/kids/kids.service';
import {Kid} from '../../../../../core/types/dtos/kid';
import { ShopItemStatusStatus } from '../../../../../core/types/enums/shop-item-status';
import {FormBuilder, FormControl, Validators} from '@angular/forms';
import {ShopFormGroup} from '../../../../../shared/types/shop-form-group';
import { SHOP_EMOJIS } from '../../../../../core/consts/shop-emojis';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-edit-shop',
  imports: [ShopForm],
  templateUrl: './edit-shop.html',
})
export class EditShop {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);

  private shopService = inject(ShopService);
  private kidsService = inject(KidsService);

  loading = signal(false);
  error = signal<string | null>(null);

  shopItemId!: number;

  kidsSignal = this.kidsService.kids;

  selectedKids: Kid[] = [];

  location = inject(Location);

  SHOP_EMOJIS = SHOP_EMOJIS;

  stockDisplay = 'Unlimited';

  form = this.fb.group<ShopFormGroup>({
    assignedKids: new FormControl([], {
      validators: [Validators.required],
      nonNullable: true,
    }),
    icon: new FormControl('⚡', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    description: new FormControl(''),
    category: new FormControl('Other', { validators: [Validators.required], nonNullable: true }),
    cost: new FormControl(0, { validators: [Validators.required], nonNullable: true }),
    quantity: new FormControl(null as number | null),
    status: new FormControl(ShopItemStatusStatus.Available, {
      validators: [Validators.required],
      nonNullable: true,
    }),
  });

  constructor() {
    effect(() => {
      const shopItem = this.shopService.shopItems().find((s) => s.shopItemId === this.shopItemId);

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
    this.shopService
      .updateShopItem$({ ...this.form.getRawValue(), shopItemId: this.shopItemId })
      .subscribe({
        next: () => {
          this.loading.set(false);
          window.history.back();
        },
        error: () => {
          this.error.set('Failed to update chore. Please try again.');
          this.loading.set(false);
          window.history.back();
        },
      });
  }

  selectKid(kidId: number) {
    this.selectedKids = this.kidsSignal().filter((kid) => kid.kidId == kidId || kidId === -1);

    this.form.patchValue({
      assignedKids: this.selectedKids.map((selectedKid) => ({
        kidId: selectedKid.kidId,
        status: ShopItemStatusStatus.Available,
      })),
    });
  }

  changeQuantity(reduce = false) {
    let currentQuantity = this.form.controls.quantity.value;

    if (currentQuantity == null) {
      if (reduce) {
        return;
      } else {
        currentQuantity = -1;
      }
    }

    const updatedQuantity = reduce ? currentQuantity - 1 : currentQuantity + 1;
    const newQuantity = updatedQuantity > -1 ? updatedQuantity : null;

    this.form.controls.quantity.patchValue(newQuantity);

    this.stockDisplay = newQuantity == null ? 'Unlimited' : newQuantity.toString();
  }
}
