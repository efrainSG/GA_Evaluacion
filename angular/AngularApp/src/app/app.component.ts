import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AngularApp';
  subtotal: number = 0;
  taxesPercentage: number = 0.0401;
  feesPercentage: number = 0.024;

  onUpdateSubTotal(data:any){
    console.log("data recibido en APP component", data);
    this.subtotal = data.subtotal;
  }
}
