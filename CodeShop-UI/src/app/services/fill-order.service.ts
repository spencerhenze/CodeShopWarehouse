import { FillOrder } from './../models/fill-order';
import { Injectable, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';


@Injectable()
export class FillOrderService {
    public subscribe = true;
    public fillOrders = new BehaviorSubject<FillOrder[]>([]);
    public baseUrl = 'http://localhost:65220/api/fillorders'

    constructor(
        public http: HttpClient
    ) {
        this.getFillOrders();
        // this.fillOrders.next([
        //     {
        //         id: 123,
        //         processedTimestamp: Date.now()
        //     },
        //     {
        //         id: 254,
        //         processedTimestamp: Date.now()
        //     }
        // ])
    }

    getFillOrders() {
        this.http
            // this route gets photos and notes. The photos controller only handles new photos and getting specific photos
            .get<FillOrder[]>(`${this.baseUrl}`)
            .subscribe(data => {
                this.fillOrders.next(data);
            });
    }

    processFillOrder(fillOrder: FillOrder) {
        this.http
        .post(`${this.baseUrl}/process`, fillOrder, {responseType: 'text'})
        .subscribe(data => {
            var updatedOrder = JSON.parse(data);
            var modifiedList = this.fillOrders.getValue();
            modifiedList[modifiedList.indexOf(fillOrder)] = updatedOrder;
            // this.getFillOrders();
        })
    }

}
