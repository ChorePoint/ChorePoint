import { Location } from '@angular/common';
import {Component, EventEmitter, inject, Input, Output} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule} from '@angular/forms';
import { RouterLink } from '@angular/router';
import {EmojiPicker} from '../../../features/chores/components/emoji-picker/emoji-picker';
import {CategorySelector} from '../category-selector/category-selector';
import {LoadingEmoji} from '../loading-emoji/loading-emoji';
import {KidAssign} from '../../../features/chores/components/kid-assign/kid-assign';
import {ShopService} from '../../../core/services/shop/shop.service';
import {KidsService} from '../../../core/services/kids/kids.service';
import {Kid} from '../../../core/types/dtos/kid';
import {ShopFormGroup} from '../../types/shop-form-group';
import { SHOP_EMOJIS } from '../../../core/consts/shop-emojis';
import {ShopItemStatusStatus} from '../../../core/types/enums/shop-item-status';

@Component({
  selector: 'app-shop-form',
  imports: [
    ReactiveFormsModule,
    RouterLink,
    EmojiPicker,
    CategorySelector,
    LoadingEmoji,
    KidAssign,
  ],
  templateUrl: './shop-form.html',
  styleUrl: './shop-form.scss',
})
export class ShopForm {
  private fb = inject(FormBuilder);
  private shopService = inject(ShopService);
  private kidsService = inject(KidsService);

  @Input({ required: true }) form!: FormGroup<ShopFormGroup>;
  @Input() loading = false;
  @Input() title = 'Add Item';
  @Input() submitText = 'Save Item';
  @Input() kids!: Kid[];

  @Output() submitted = new EventEmitter<void>();

  selectedKids: Kid[] = [];

  location = inject(Location);

  SHOP_EMOJIS = SHOP_EMOJIS;

  get stockDisplay(): string {
    return this.form.controls.quantity.value?.toString() ?? 'Unlimited';
  }

  submit() {
    this.submitted.emit();
  }

  selectKid(kidId: number) {
    this.selectedKids = this.kids.filter((kid) => kid.kidId == kidId || kidId === -1);

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
  }
}
