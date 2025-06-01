import { Component, inject, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { UserService } from '../../services/user.service';
import { CommonModule, DatePipe } from '@angular/common';
import { StatusPipe } from '../../shared/status.pipe';
import { ToastService } from '../../services/toast.service';
import { Status } from '../../model/order.model';
import { Router } from '@angular/router';
import { ShipmentService } from '../../services/shipment.service';
import { catchError, map, Observable, of } from 'rxjs';
import { Shipment } from '../../model/shipment.model';
import { ShippinStatusPipe } from "../../shared/shippingStatus.pipe";

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [CommonModule, StatusPipe, ShippinStatusPipe],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent implements OnInit{
  private readonly router = inject(Router);
  orderService = inject(OrderService);
  orders$ = this.orderService.orders$;
  userService = inject(UserService);
  toastService = inject(ToastService);
  shipmentService = inject(ShipmentService);
  userRole = this.userService.getUserTokenInfo().role;
  userId = this.userService.getUserTokenInfo().id;
  pendingStatus: Status = Status.PENDING;
  shipmentMap = new Map<number, Shipment[]>();


  ngOnInit(): void {
    this.reloadPage();
  }

  onOrderClicked(orderId: number){
    const element = document.getElementById(`order-info-${orderId}`);
    if(element?.style.display === 'none') element!.style.display = 'block';
    else element!.style.display = 'none';
  }

  confirmOrder(orderId: number){
    this.orderService.validateProductInAOrder(orderId,this.userId).subscribe({
      next: (x) => {
        sessionStorage.setItem(`confirmedOrder_${orderId}_${this.userId}`, "true");
        this.toastService.showSuccesToast("Order confirm!");
        this.reloadPage();
      },
      error: (e) => this.toastService.showErrorToast("Cannot confirm order!")
    })
  }

  shipOrder(orderId: number){
    this.router.navigate(['/home/orders/shipment',orderId]);
  }

  reloadPage(){
    if(this.userRole === 'Customer') this.orderService.getAllCustomerOrder(this.userId).subscribe({
      next: (orders) => {
        for (const order of orders) {
          this.shipmentService.getAllShipmentOfAnOrder(order.id).subscribe(shipments => {
          this.shipmentMap.set(order.id, shipments);});
        }
      }
    });
    else if (this.userRole === 'Artisan') {
      this.orderService.getAllArtisanOrder(this.userId).subscribe();
      
    }
  }

  getShipmentOfAnOrder(orderId: number): Observable<Shipment[]>{
    return this.shipmentService.getAllShipmentOfAnOrder(orderId).pipe(
      map((shipments: Shipment[]) => shipments)
    );
  }

  isOrderConfirmed(orderId: number): boolean {
  return sessionStorage.getItem(`confirmedOrder_${orderId}_${this.userId}`) === "true";
  }

  isOrderShipped(orderId: number){
    return sessionStorage.getItem(`shippedOrder_${orderId}_${this.userId}`)  === "true";
  }

  getShipmentForProduct(shipments: Shipment[], productRef: number): Shipment | null {
  return shipments.find(s => s.products.includes(productRef)) ?? null;
  }

}
