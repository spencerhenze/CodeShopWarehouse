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
}
