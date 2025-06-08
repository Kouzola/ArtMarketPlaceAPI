import { Component, inject, OnInit } from '@angular/core';
import { ToastService } from '../../services/toast.service';
import { CardComponent } from "../../shared/card/card.component";
import { UserService } from '../../services/user.service';
import { OrderService } from '../../services/order.service';
import { CommonModule } from '@angular/common';
import { ShipmentService } from '../../services/shipment.service';
import { ShipmentStatus } from '../../model/shipment.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CardComponent, CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit{
  userService = inject(UserService);
  orderService = inject(OrderService);
  shipmentService = inject(ShipmentService);
  userRole = this.userService.getUserTokenInfo().role;
  userId = 0;
  displayedOrderCount = 0;
  targetOrderCount = 0;
  displayDeliveredShipmentCount = 0;
  targetDeliveredShipmentCount = 0;


  ngOnInit(): void {  
  this.userId = this.userService.getUserTokenInfo().id;
    if(this.userRole === 'Artisan'){
      this.orderService.getAllArtisanOrder(this.userId).subscribe({
        next: (orders) => {
        this.targetOrderCount = orders.length;
        this.updateOrderCounter();
        }});
    } else if(this.userRole === 'Delivery'){
      this.shipmentService.getAllShipmentOfPartnerId(this.userId).subscribe({
        next: (shipments) => {
          this.targetDeliveredShipmentCount = shipments.filter(sh => sh.status == ShipmentStatus.DELIVERED).length;
          this.updateShipmentCounter();
        }
      })
    }
  }

  updateOrderCounter(){
    const counter = document.querySelector('order-count');
    const increment = this.targetOrderCount / 30;
    const intervalTime = 20;

    const interval = setInterval(() => {
    if (this.displayedOrderCount + increment >= this.targetOrderCount) {
      this.displayedOrderCount = this.targetOrderCount;
      clearInterval(interval);
    } else {
      this.displayedOrderCount += increment;
    }

  }, intervalTime);
  }

  updateShipmentCounter(){
    const counter = document.querySelector('shipment-count');
    const increment = this.targetDeliveredShipmentCount / 30;
    const intervalTime = 20;

    const interval = setInterval(() => {
    if (this.displayDeliveredShipmentCount + increment >= this.targetDeliveredShipmentCount) {
      this.displayDeliveredShipmentCount = this.targetDeliveredShipmentCount;
      clearInterval(interval);
    } else {
      this.displayDeliveredShipmentCount += increment;
    }

  }, intervalTime);
  }
  
}


