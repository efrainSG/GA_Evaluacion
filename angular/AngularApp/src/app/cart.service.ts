import {HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ItemModel } from './item-model';
import { Observable, catchError, of } from 'rxjs';
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
    return this.httpClient.get<any>(url)
    .pipe(
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
