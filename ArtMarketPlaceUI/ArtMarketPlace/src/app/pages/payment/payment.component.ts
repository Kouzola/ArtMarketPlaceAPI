import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { PaymentDetail } from '../../model/paymentDetail.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit{
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  orderService = inject(OrderService);
  toastService = inject(ToastService);
  price: number = 0;
  selectedPaymentMethod: string = "BANCONTACT";
  orderId = 0;

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.orderService.getOrderTotalPrice(Number(params.get('orderId')!)).subscribe(p => this.price = p),
      this.orderId = Number(params.get('orderId')!);
    })
  }

  payOrder(){
    const paymentDetail: PaymentDetail = {paymentMethod: this.selectedPaymentMethod, amount: this.price}
    this.orderService.payOrder(this.orderId,paymentDetail).subscribe({
      next: s => {
        this.toastService.showSuccesToast("Order Payed!");
        this.router.navigate(['/home/orders']);
      },
      error: e => this.toastService.showErrorToast("Order not payed!")
    });
    
  }

  cancelOrder(){
    this.orderService.cancelOrder(this.orderId).subscribe(v => console.log(v));
    this.router.navigate(['/home/cart']);
  }
  
}
