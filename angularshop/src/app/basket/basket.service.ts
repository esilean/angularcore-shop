import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import {
  Basket,
  BasketItem,
  BasketTotals,
  CustomerBasket,
} from '../shared/models/basket';
import { Product } from '../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseUrl: string = environment.apiUrl;

  private basketSource = new BehaviorSubject<Basket>(null);
  basket$ = this.basketSource.asObservable();

  private basketTotalsSource = new BehaviorSubject<BasketTotals>(null);
  basketTotals$ = this.basketTotalsSource.asObservable();

  constructor(private http: HttpClient) {}

  getBasket(id: string) {
    return this.http.get(this.baseUrl + 'basket/' + id).pipe(
      map((basket: Basket) => {
        this.basketSource.next(basket);
        this.calculateTotals();
      })
    );
  }

  setBasket(basket: Basket) {
    return this.http.post(this.baseUrl + 'basket', basket).subscribe(
      (response: Basket) => {
        this.basketSource.next(response);
        this.calculateTotals();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getCurrentBasketValue(): Basket {
    return this.basketSource.value;
  }

  addItemBasket(item: Product, quantity: number = 1) {
    const itemToAdd: BasketItem = this.mapProductItemToBasketItem(
      item,
      quantity
    );

    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);

    this.setBasket(basket);
  }

  incrementItemQuantity(item: BasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex((x) => x.id === item.id);
    basket.items[foundItemIndex].quantity++;
    this.setBasket(basket);
  }

  decrementItemQuantity(item: BasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex((x) => x.id === item.id);
    if (basket.items[foundItemIndex].quantity > 1) {
      basket.items[foundItemIndex].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }
  }
  
  removeItemFromBasket(item: BasketItem) {
    const basket = this.getCurrentBasketValue();
    if (basket.items.some((x) => x.id === item.id)) {
      basket.items = basket.items.filter((x) => x.id !== item.id);
      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }
  }

  deleteBasket(basket: Basket) {
    return this.http.delete(this.baseUrl + 'basket/' + basket.id).subscribe(
      () => {
        this.basketSource.next(null);
        this.basketTotalsSource.next(null);
        localStorage.removeItem('basket_id');
      },
      (error) => {
        console.log(error);
      }
    );
  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    const shipping: number = 0;
    const subtotal: number = basket.items.reduce(
      (a, b) => b.price * b.quantity + a,
      0
    );
    const total: number = shipping + subtotal;

    this.basketTotalsSource.next({ shipping, subtotal, total });
  }

  private addOrUpdateItem(
    items: BasketItem[],
    itemToAdd: BasketItem,
    quantity: number
  ): BasketItem[] {
    const index = items.findIndex((i) => i.id == itemToAdd.id);

    if (index === -1) {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    } else {
      items[index].quantity += quantity;
    }

    return items;
  }

  private createBasket(): Basket {
    const basket = new CustomerBasket();
    console.log(basket.id);
    localStorage.setItem('basket_id', basket.id);

    return basket;
  }

  private mapProductItemToBasketItem(
    item: Product,
    quantity: number
  ): BasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity,
      pictureUrl: item.pictureUrl,
      brand: item.productBrand,
      type: item.productType,
    } as BasketItem;
  }
}
