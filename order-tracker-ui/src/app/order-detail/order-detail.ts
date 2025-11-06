import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { forkJoin } from 'rxjs';
import { Order, OrderService, OrderStatusHistory } from '../services/order';

@Component({
  selector: 'app-order-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './order-detail.html',
  styleUrl: './order-detail.css',
})
export class OrderDetailComponent implements OnInit {
  order: Order | null = null;
  history: OrderStatusHistory[] = [];
  loading = true;
  error: string | null = null;
  orderId: string = '';
  successMessage: string | null = null;

  constructor(private route: ActivatedRoute, private orderService: OrderService) {}

  ngOnInit() {
    this.orderId = this.route.snapshot.paramMap.get('id') || '';
    this.loadOrderData();
  }

  loadOrderData() {
    this.loading = true;
    this.error = null;

    forkJoin({
      order: this.orderService.getOrder(this.orderId),
      history: this.orderService.getOrderHistory(this.orderId),
    }).subscribe({
      next: (data) => {
        this.order = data.order;
        this.history = data.history;
        this.loading = false;
      },
      error: (err) => {
        this.error =
          err.status === 404 ? `Order ${this.orderId} not found!` : 'Failed to load order';
        this.loading = false;
      },
    });
  }

  advanceStatus() {
    if (!this.orderId) return;

    this.orderService.advanceStatus(this.orderId).subscribe({
      next: () => {
        this.successMessage = 'Order status advanced!';
        setTimeout(() => (this.successMessage = null), 3000);
        this.loadOrderData();
      },
      error: (err) => (this.error = 'Failed to advance status'),
    });
  }

  getStatusName(status: number): string {
    const statuses = ['Pending', 'Processing', 'Shipped', 'Delivered', 'Cancelled'];
    return statuses[status] || 'Unknown';
  }
}
