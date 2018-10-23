import { FillOrderService } from './services/fill-order.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule

  ],
  providers: [
      FillOrderService,
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
