import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { OrderSummaryComponent } from './order-summary/order-summary.component';
import { OrderTotalComponent } from './order-total/order-total.component';
import { OrderItemComponent } from './order-item/order-item.component';

@NgModule({
  declarations: [
    AppComponent,
    OrderSummaryComponent,
    OrderTotalComponent,
    OrderItemComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
