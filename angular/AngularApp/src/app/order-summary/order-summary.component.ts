import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CartService } from '../cart.service';
import { ItemCartModel } from '../item-cart-model';

@Component({
  selector: 'app-order-summary',
  templateUrl: './order-summary.component.html',
  styleUrls: ['./order-summary.component.css']
})
export class OrderSummaryComponent implements OnInit  {

  items?: ItemCartModel[]
  subtotal:number = 0;

  @Output() newSubtotal = new EventEmitter<any>()
  @Output() newDelSubtotal = new EventEmitter<any>()

  constructor(private readonly servicio: CartService) {

  }

  ngOnInit(): void {
    this.servicio.getItems().subscribe(o => {
      this.items = o;
      this.update();
    });
  }

  removeAll() {
    this.servicio.delItems().subscribe(o => {
      this.items = o;
      this.newSubtotal.emit({subtotal: 0});
    });
  }

  onUpdate(data:ItemCartModel){
      this.update();
  }

  onDelete(data:ItemCartModel){
    let index = this.items?.indexOf(data);
    console.log("on delete", {index, data});
  }

  private update() {
    if(this.items){
      this.subtotal = (this.items) ? this.items.reduce((total, item) => total + item.total!, 0) : 0;
    }
    this.newSubtotal.emit({subtotal: this.subtotal});
  }
}
