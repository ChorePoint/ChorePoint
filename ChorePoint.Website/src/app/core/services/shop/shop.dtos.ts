import { AssignedKidToShopItem } from '../../types/dtos/assigned-kid-to-shop-item';
import { ShopItemStatusStatus } from '../../types/enums/shop-item-status';
import {CreateChoreRequest} from '../chore/chore.dtos';

export interface NewShopItemRequest {
  assignedKids: AssignedKidToShopItem[];
  name: string;
  icon: string;
  description: string | null;
  category: string;
  cost: number;
  status: ShopItemStatusStatus;
  quantity: number | null;
}

export type UpdateShopItemRequest = NewShopItemRequest & { shopItemId: number };

