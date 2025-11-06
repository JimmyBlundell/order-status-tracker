import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-order-search',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './order-search.html',
  styleUrl: './order-search.css',
})
export class OrderSearchComponent {
  orderNumber: string = '';

  constructor(private router: Router) {}

  onSubmit() {
    if (this.orderNumber.trim()) {
      this.router.navigate(['/order', this.orderNumber]);
    }
  }
}
