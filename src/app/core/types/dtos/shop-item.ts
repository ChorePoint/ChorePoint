import { ShopItemStatusStatus } from '../enums/shop-item-status';

export interface ShopItem {
  id: number;
  parentId: number;
  kidId: number;
  name: string;
  description: string;
  cost: number;
  status: ShopItemStatusStatus;
  quantity: number;
  createdAt: Date | null;
  updatedAt: Date | null;
}
