import { FormControl } from '@angular/forms';
import { AssignedKidToShopItem } from '../../dtos/parent/assigned-kid-to-shop-item';

export interface ShopFormGroup {
  assignedKids: FormControl<AssignedKidToShopItem[]>;
  icon: FormControl<string>;
  name: FormControl<string>;
  description: FormControl<string | null>;
  category: FormControl<string>;
  cost: FormControl<number>;
  quantity: FormControl<number | null>;
  isVisible: FormControl<boolean>;
}
