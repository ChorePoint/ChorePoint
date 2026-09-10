import {Location} from '@angular/common';
import {Component, effect, EventEmitter, inject, Input, Output, signal} from '@angular/core';
import {FormGroup, ReactiveFormsModule} from '@angular/forms';
import {RouterLink} from '@angular/router';
import {EmojiPicker} from '../../../features/chores/components/emoji-picker/emoji-picker';
import {CategorySelector} from '../category-selector/category-selector';
import {LoadingEmoji} from '../loading-emoji/loading-emoji';
import {KidAssign} from '../../../features/chores/components/kid-assign/kid-assign';
import {Kid} from '../../../core/types/dtos/kid';
import {ShopFormGroup} from '../../types/shop-form-group';
import {SHOP_EMOJIS} from '../../../core/consts/shop-emojis';
import {ShopItemStatusStatus} from '../../../core/types/enums/shop-item-status';
import {ToastPopup} from '../toast-popup/toast-popup';

@Component({
  selector: 'app-shop-form',
  imports: [
    ReactiveFormsModule,
    RouterLink,
    EmojiPicker,
    CategorySelector,
    LoadingEmoji,
    KidAssign,
    ToastPopup,
  ],
  templateUrl: './shop-form.html',
  styleUrl: './shop-form.scss',
})
export class ShopForm {
  @Input({ required: true }) form!: FormGroup<ShopFormGroup>;
  @Input() loading = false;
  @Input() title = 'Add Item';
  @Input() submitText = 'Save Item';
  @Input() kids!: Kid[];
  @Input() error = signal<string | null>(null);

  @Output() submitted = new EventEmitter<void>();

  toastState = {
    visible: false,
    text: '✓ Changes saved',
    success: true,
  };

  selectedKids: Kid[] = [];

  location = inject(Location);

  SHOP_EMOJIS = SHOP_EMOJIS;

  constructor() {
    effect(() => {
      const error = this.error();
      if (error) {
        this.toastState = {
          visible: true,
          text: error,
          success: false,
        };

        this.showToast();
      }
    });
  }

  showToast() {
    this.toastState.visible = true;
    setTimeout(() => {
      this.toastState.visible = false;
    }, 2000);
  }

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
