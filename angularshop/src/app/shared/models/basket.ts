import { v4 as uuidV4 } from 'uuid';

export interface Basket {
  id: string;
  items: BasketItem[];
}

export interface BasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  type: string;
}

export interface BasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}

export class CustomerBasket implements Basket {
  id = uuidV4();
  items: BasketItem[] = [];
}
