import { FillOrder } from './../../models/fill-order';
import { FillOrderService } from './../../services/fill-order.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';


@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
  })

export class HomeComponent {
  title = 'home';
  fillOrders: FillOrder[];
  subscribe = true;

  constructor(
      public fillOrderService: FillOrderService,
      public router: Router
  ) {
    this.fillOrderService.fillOrders.takeWhile(() => this.subscribe).subscribe(fillOrders => {
        this.fillOrders = fillOrders;
    });
  }

  processFillOrder(fillOrder: FillOrder) {
      this.fillOrderService.processFillOrder(fillOrder);
  }

  goToDetailPage(order: FillOrder) {
    const queryParams = {
        'order': order
    }
      this.router.navigate([`orders/${order.id}/details`], {queryParams})
  }
}

