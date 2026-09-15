import { AssignedKidToShopItem } from '../../types/dtos/assigned-kid-to-shop-item';

export interface NewShopItemRequest {
  assignedKids: AssignedKidToShopItem[];
  name: string;
  icon: string;
  description: string | null;
  category: string;
  cost: number;
  quantity: number | null;
}

export type UpdateShopItemRequest = NewShopItemRequest & { shopItemId: number };

