import { FormControl } from '@angular/forms';
import { AssignedKidToShopItem } from '../../core/types/dtos/assigned-kid-to-shop-item';
import { ShopItemStatusStatus } from '../../core/types/enums/shop-item-status';

export interface ShopFormGroup {
  assignedKids: FormControl<AssignedKidToShopItem[]>;
  icon: FormControl<string>;
  name: FormControl<string>;
  description: FormControl<string | null>;
  category: FormControl<string>;
  cost: FormControl<number>;
  quantity: FormControl<number | null>;
  status: FormControl<ShopItemStatusStatus>;
}
