import { ShopItemStatus } from '../shop-item-status';

export enum ShopItemStatusStatus {
  Available,
  Pending,
  Hidden,
}

export const SHOP_ITEM_STATUS_MAP: Record<ShopItemStatusStatus, ShopItemStatus> = {
  [ShopItemStatusStatus.Available]: {
    name: 'available',
    icon: '✅',
  },
  [ShopItemStatusStatus.Pending]: {
    name: 'pending',
    icon: '⌛',
  },
  [ShopItemStatusStatus.Hidden]: {
    name: 'hidden',
    icon: '🙈',
  },
};
