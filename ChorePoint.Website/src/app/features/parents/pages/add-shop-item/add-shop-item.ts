import { Location } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { SHOP_EMOJIS } from '../../../../core/consts/shop-emojis';
import { KidsService } from '../../../../core/services/kids/kids.service';
import { ShopService } from '../../../../core/services/shop/shop.service';
import { Kid } from '../../../../core/types/dtos/kid';
import { ShopItemStatusStatus } from '../../../../core/types/enums/shop-item-status';
import { CategorySelector } from '../../../../shared/components/category-selector/category-selector';
import { LoadingEmoji } from '../../../../shared/components/loading-emoji/loading-emoji';
import { ShopFormGroup } from '../../../../shared/types/shop-form-group';
import { EmojiPicker } from '../../../chores/components/emoji-picker/emoji-picker';
import { KidAssign } from '../../../chores/components/kid-assign/kid-assign';

@Component({
  selector: 'app-add-shop-item',
  imports: [
    ReactiveFormsModule,
    RouterLink,
    EmojiPicker,
    CategorySelector,
    LoadingEmoji,
    KidAssign,
  ],
  templateUrl: './add-shop-item.html',
  styleUrl: './add-shop-item.scss',
})
export class AddShopItem implements OnInit {
  private fb = inject(FormBuilder);
  private shopService = inject(ShopService);
  private kidsService = inject(KidsService);

  kidsSignal = this.kidsService.kids;

  selectedKids: Kid[] = [];

  location = inject(Location);

  loading = false;

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

    this.loading = true;

    const formData = this.form.getRawValue();

    this.shopService.newShopItem$(formData).subscribe({
      next: () => {
        this.loading = false;
        this.form.reset();
        this.location.back();
      },
      error: () => {
        this.loading = false;
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
