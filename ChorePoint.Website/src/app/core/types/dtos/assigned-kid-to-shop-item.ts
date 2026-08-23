import { ShopItemStatusStatus } from '../enums/shop-item-status';

export interface AssignedKidToShopItem {
  kidId: number;
  status: ShopItemStatusStatus;
}
