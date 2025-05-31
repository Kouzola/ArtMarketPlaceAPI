import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ShipmentService } from '../../services/shipment.service';
import { Router } from '@angular/router';
import { ToastService } from '../../services/toast.service';
import { UserService } from '../../services/user.service';
import { StatusPipe } from "../../shared/status.pipe";
import { ShipmentStatus } from '../../model/shipment.model';
import { ShippinStatusPipe } from "../../shared/shippingStatus.pipe";
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-shipment',
  standalone: true,
  imports: [CommonModule, FormsModule, ShippinStatusPipe],
  templateUrl: './shipment.component.html',
  styleUrl: './shipment.component.css'
})
export class ShipmentComponent implements OnInit{
  private readonly router = inject(Router);
  userService = inject(UserService);
  toastService = inject(ToastService);
  shipmentService = inject(ShipmentService);
  orderService = inject(OrderService);
  userRole = this.userService.getUserTokenInfo().role;
  userId = this.userService.getUserTokenInfo().id;
  shipments$ = this.shipmentService.shipments$;
  orders$ = this.orderService.orders$;

  ngOnInit(): void {
    this.reloadPage();
  }

  updateShippingStatus(shipmentId: number, shipmentStatus: ShipmentStatus){
    this.shipmentService.updateShipmentDeliveryStatus(shipmentId, shipmentStatus).subscribe({
      next: (s) => {
        this.toastService.showSuccesToast("Product Status updated!");
        this.reloadPage();
      },
      error: (e) => this.toastService.showErrorToast("Product Status not updated!")
    })
  }
  
  reloadPage(){
    this.shipmentService.getAllShipmentOfPartnerId(this.userId).subscribe();
  }
}
