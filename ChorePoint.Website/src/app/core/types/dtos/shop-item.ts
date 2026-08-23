import { AssignedKidToShopItem } from './assigned-kid-to-shop-item';

export interface ShopItem {
  shopItemId: number;
  parentId: number;
  assignedKids: AssignedKidToShopItem[];
  name: string;
  description: string;
  cost: number;
  quantity: number;
  createdAt: Date | null;
  updatedAt: Date | null;
}

export type ShopItemCard = ShopItem & { assignedKidsString: string };
