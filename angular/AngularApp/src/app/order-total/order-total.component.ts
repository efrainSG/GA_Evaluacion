import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-order-total',
  templateUrl: './order-total.component.html',
  styleUrls: ['./order-total.component.css']
})
export class OrderTotalComponent implements OnChanges, OnInit {
  @Input() subtotal?:number = 0.00;
  @Input() taxesPercentage?:number = 0.00;
  @Input() feesPercentage?:number = 0.00;
  
  fees?:number = 0.00;
  taxes?:number = 0.00;
  total?:number = 0.00;

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.fees = this.subtotal! * this.feesPercentage!;
    this.taxes = (this.subtotal! + this.fees!) * this.taxesPercentage!;

    this.total = this.subtotal! + this.fees! + this.taxes!;
  }
}
