import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { ItemCartModel } from '../item-cart-model';

@Component({
  selector: 'app-order-item',
  templateUrl: './order-item.component.html',
  styleUrls: ['./order-item.component.css']
})
export class OrderItemComponent implements OnInit {
@Input() Item?: ItemCartModel;
@Output() newItem = new EventEmitter<any>();
@Output() delItem = new EventEmitter<any>();

ngOnInit(): void {
  if (isNaN(this.Item!.quantity!)){
    this.Item!.quantity! = 1;
  }
  this.Item!.total = !isNaN(this.Item!.price * this.Item!.quantity!) ? (this.Item!.price * this.Item!.quantity!): 0.00;
}

onSubtract(){
  if (this.Item?.quantity! > 0) {
    this.Item!.quantity! -= 1;
  }
  this.Item!.total = !isNaN(this.Item!.price * this.Item!.quantity!) ? (this.Item!.price * this.Item!.quantity!): 0.00;
  this.newItem.emit({item: this.Item!});
}

onAdd(){
    this.Item!.quantity! += 1;
    this.Item!.total = !isNaN(this.Item!.price * this.Item!.quantity!) ? (this.Item!.price * this.Item!.quantity!): 0.00;
    this.newItem.emit({item: this.Item!});
  }

onRemove(){
  this.delItem.emit({item: this.Item!});
}
}
