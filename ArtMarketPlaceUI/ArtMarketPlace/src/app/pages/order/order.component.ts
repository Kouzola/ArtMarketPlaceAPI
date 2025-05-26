import { Component, inject, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { UserService } from '../../services/user.service';
import { CommonModule, DatePipe } from '@angular/common';
import { StatusPipe } from '../../shared/status.pipe';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [CommonModule, StatusPipe],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent implements OnInit{
  
  orderService = inject(OrderService);
  orders$ = this.orderService.orders$;
  userService = inject(UserService);

  ngOnInit(): void {
    this.orderService.getAllCustomerOrder(this.userService.getUserTokenInfo().id).subscribe();
  }

  onOrderClicked(orderId: number){
    const element = document.getElementById(`order-info-${orderId}`);
    if(element?.style.display === 'none') element!.style.display = 'block';
    else element!.style.display = 'none';
  }

  //TODO Si Artiste changer comment on affiche order

}
