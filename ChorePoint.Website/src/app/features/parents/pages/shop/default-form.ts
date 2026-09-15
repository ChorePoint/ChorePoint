import {ShopFormGroup} from '../../../../shared/types/shop-form-group';
import {FormControl, FormGroup, Validators} from '@angular/forms';

export function createDefaultShopItemForm(): FormGroup<ShopFormGroup> {
  return new FormGroup<ShopFormGroup>({
    assignedKids: new FormControl([], {
      validators: [Validators.required],
      nonNullable: true,
    }),
    icon: new FormControl('⚡', {
      validators: [Validators.required],
      nonNullable: true,
    }),
    name: new FormControl('', {
      validators: [Validators.required],
      nonNullable: true,
    }),
    description: new FormControl(''),
    category: new FormControl('Other', {
      validators: [Validators.required],
      nonNullable: true,
    }),
    cost: new FormControl(0, {
      validators: [Validators.required],
      nonNullable: true,
    }),
    quantity: new FormControl(null as number | null),
    isVisible: new FormControl(true, {
      validators: [Validators.required],
      nonNullable: true,
    }),
  });
}
