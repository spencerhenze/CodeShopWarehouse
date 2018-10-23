import { HomeComponent } from './pages/home/home.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { OrderDetailComponent } from './pages/order-detail/order-detail.component';



const routes: Routes = [
    { path: 'orders/:id/details', component: OrderDetailComponent },
    { path: 'home', component: HomeComponent },
  ];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ]
})


export class AppRoutingModule { }
