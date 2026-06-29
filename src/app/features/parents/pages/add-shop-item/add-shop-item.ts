import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { SHOP_EMOJIS } from '../../../../core/consts/shop-emojis';
import { ShopService } from '../../../../core/services/shop/shop.service';
import { ShopItemStatusStatus } from '../../../../core/types/enums/shop-item-status';
import { CategorySelector } from '../../../../shared/components/category-selector/category-selector';
import { LoadingEmoji } from '../../../../shared/components/loading-emoji/loading-emoji';
import { EmojiPicker } from '../../../chores/components/emoji-picker/emoji-picker';

@Component({
  selector: 'app-add-shop-item',
  imports: [ReactiveFormsModule, RouterLink, EmojiPicker, CategorySelector, LoadingEmoji],
  templateUrl: './add-shop-item.html',
  styleUrl: './add-shop-item.scss',
})
export class AddShopItem {
  private fb = inject(FormBuilder);
  private shopService = inject(ShopService);

  loading = false;

  SHOP_EMOJIS = SHOP_EMOJIS;

  stockDisplay = 'Unlimited';

  form = this.fb.nonNullable.group({
    kidId: [1], // Temporary field as endpoint will be changing
    icon: ['⚡', { validators: [Validators.required] }],
    name: ['', { validators: [Validators.required] }],
    description: [''],
    category: ['Other', { validators: [Validators.required] }],
    cost: [0, { validators: [Validators.required] }],
    quantity: [null as number | null, { validators: [Validators.required] }], // Endpoint not currently accepting nullable but will in
    status: [ShopItemStatusStatus.Available, { validators: [Validators.required] }],
  });

  submit() {
    console.log(this.form.invalid);
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    const formData = this.form.getRawValue();

    this.shopService.newShopItem$(formData).subscribe({
      next: () => {
        this.loading = false;
        this.form.reset();
      },
      error: () => {
        this.loading = false;
      },
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
