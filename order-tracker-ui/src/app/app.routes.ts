import { Routes } from '@angular/router';
import { OrderDetailComponent } from './order-detail/order-detail';
import { OrderSearchComponent } from './order-search/order-search';

export const routes: Routes = [
  { path: '', component: OrderSearchComponent },
  { path: 'order/:id', component: OrderDetailComponent },
];
