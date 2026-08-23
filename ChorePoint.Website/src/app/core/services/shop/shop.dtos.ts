import { AssignedKidToShopItem } from '../../types/dtos/assigned-kid-to-shop-item';
import { ShopItemStatusStatus } from '../../types/enums/shop-item-status';

export interface NewShopItemRequest {
  assignedKids: AssignedKidToShopItem[];
  name: string;
  description: string | null;
  cost: number;
  status: ShopItemStatusStatus;
  quantity: number | null;
}
