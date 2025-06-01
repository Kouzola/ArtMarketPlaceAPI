import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OrderService } from '../../../services/order.service';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-order-shipment',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,FormsModule],
  templateUrl: './order-shipment.component.html',
  styleUrl: './order-shipment.component.css'
})
export class OrderShipmentComponent implements OnInit{
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  userService = inject(UserService);
  orderService = inject(OrderService);
  toastService = inject(ToastService);
  deliveryPartners$ = this.userService.users$;
  selectedDeliveryPartnerId = 0;
  orderId = 0;


  ngOnInit(): void {
    this.userService.getAllDeliveryGuy().subscribe();
    this.route.paramMap.subscribe(params => {
      this.orderId = Number(parseInt(params.get('orderId')!));
    })
  }

  selectDeliveryPartner(){
    this.orderService.shipOrder(this.orderId,{deliveryPartnerId: this.selectedDeliveryPartnerId, artisanId: this.userService.getUserTokenInfo().id}).subscribe({
      next: (s) => {
        sessionStorage.setItem(`shippedOrder_${this.orderId}_${this.userService.getUserTokenInfo().id}`, "true");
        this.toastService.showSuccesToast("Packet(s) ready to be shipped!")
        this.router.navigate(['home/orders']);
      },
      error: (e) => {
        this.router.navigate(['home/orders']);
        this.toastService.showErrorToast("Order already ready to be shipped!")
      }
    });
  }




}
