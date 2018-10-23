import { FillOrder } from './models/fill-order';
import { FillOrderService } from './services/fill-order.service';
import { Component } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  fillOrders: FillOrder[];
  subscribe = true;
  newOrderTracker = 100;

  constructor(
      public fillOrderService: FillOrderService
  ) {
    this.fillOrderService.fillOrders.takeWhile(() => this.subscribe).subscribe(fillOrders => {
        this.fillOrders = fillOrders;
    });
  }

  processFillOrder(fillOrder: FillOrder) {
      this.fillOrderService.processFillOrder(fillOrder);
  }

  createNewOrder() {
      const newOrder: FillOrder = {
          id: this.newOrderTracker,
          processedTimestamp: null
      }
      this.fillOrderService.createNewOrder(newOrder).subscribe(response => {
        this.fillOrders.unshift(newOrder);
        this.newOrderTracker++;  
      }, (error) => {
          console.log('create new order failed');
      })
  }
}
