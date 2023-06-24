import {HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ItemModel } from './item-model';
import { Observable, map, catchError, of } from 'rxjs';
import { ItemCartModel } from './item-cart-model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private items?: ItemCartModel[];

  constructor(private readonly httpClient: HttpClient) {
   }

   getItems(): Observable<any> {
    const url = 'assets/items.json';
    return this.httpClient.get<ItemModel[]>(url)
    .pipe(
      map(items => items.map(item => ({
        id: item.id,
        name: item.item_name,
        description: item.short_description,
        img: item.img,
        price: item.price_without_tax,
        quantity: item.quantity,
        tax: item.tax,
        fee: item.shipping_fee,
        total: (item.price_without_tax * item.quantity)
      }))),
      catchError((error:any) => {
        console.error('Error al leer datos desde origen', error);
        return [];
      })
    );
   }

   delItems(): Observable<ItemCartModel[]> {
    this.items = [];
    return of (this.items);
   }
}
