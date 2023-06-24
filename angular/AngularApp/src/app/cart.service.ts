import { Injectable } from '@angular/core';
import { ItemModel } from './item-model';
import { Observable, of } from 'rxjs';
import { ItemCartModel } from './item-cart-model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private items: ItemCartModel[];

  constructor() {
    this.items = [
      {id:1, name:"Item name 001", img:"./", description:"Description of item 001", price: 19.32, quantity: 1, total: 19.32 },
      {id:2, name:"Item name 002", img:"./", description:"Description of item 002", price: 10.18, quantity: 1, total: 10.18 },
      {id:3, name:"Item name 003", img:"./", description:"Description of item 003", price: 59.11, quantity: 1, total: 59.11 },
      {id:4, name:"Item name 004", img:"./", description:"Description of item 004", price: 45.60, quantity: 1, total: 45.60 }
    ];
   }

   getItems(): Observable<ItemCartModel[]> {
    return of(this.items);
   }

   delItems(): Observable<ItemCartModel[]> {
    this.items = [];
    return of (this.items);
   }
}
