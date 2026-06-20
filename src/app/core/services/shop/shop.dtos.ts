import { ShopItemStatus } from '../../types/enums/shop-item-status';

export interface NewShopItemRequest {
  kidId: number;
  name: string;
  description: string;
  cost: number;
  status: ShopItemStatus;
  quantity: number | null;
}
