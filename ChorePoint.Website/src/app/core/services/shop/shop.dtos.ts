import { ShopItemStatusStatus } from '../../types/enums/shop-item-status';

export interface NewShopItemRequest {
  kidId: number;
  name: string;
  description: string;
  cost: number;
  status: ShopItemStatusStatus;
  quantity: number | null;
}
