import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Order {
  id: number;
  orderNumber: string;
  customerId: string;
  status: number;
  orderDate: string;
  totalAmount: number;
  description?: string;
}

export interface OrderStatusHistory {
  id: number;
  orderId: number;
  status: number;
  timestamp: string;
  notes: string;
}

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private apiUrl = 'http://localhost:5075/api/orders';

  constructor(private http: HttpClient) {}

  getOrder(orderId: string): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/${orderId}`);
  }

  getOrderHistory(orderId: string): Observable<OrderStatusHistory[]> {
    return this.http.get<OrderStatusHistory[]>(`${this.apiUrl}/${orderId}/history`);
  }

  advanceStatus(orderId: string): Observable<Order> {
    return this.http.post<Order>(`${this.apiUrl}/${orderId}/advance-status`, {});
  }
}
