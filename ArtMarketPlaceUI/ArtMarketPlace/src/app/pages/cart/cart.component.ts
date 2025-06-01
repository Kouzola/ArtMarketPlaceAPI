import { Component, inject, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CartService } from '../../services/cart.service';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { Cart, CartItem } from '../../model/cart.model';
import { Router } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { ShippingOption } from '../../model/order.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit{
  
  apiUrl= environment.apiUrl;
  private readonly router = inject(Router);
  cartService = inject(CartService);
  userService = inject(UserService);
  orderService = inject(OrderService);
  toastService = inject(ToastService);
  cart$ = this.cartService.cart$;
  userId:number  = 0;
  selectedShippingOption: ShippingOption = ShippingOption.NORMAL;
  
  
  ngOnInit(): void {
    this.userId = this.userService.getUserTokenInfo().id;
    this.cartService.GetCustomerCart(this.userId).subscribe();
  }

  getTotalPrice(cart: Cart){
    let totalPrice = 0;
    cart.items.forEach(
      item => totalPrice += item.productPrice
    );
    return parseFloat(totalPrice.toFixed(2));
  }

  confirmOrder(cartId: number){
    this.orderService.createOrderFromCart({cartId: cartId, customerId: this.userId, shippingOption: this.selectedShippingOption}).subscribe({
      next: o => this.router.navigate(['home/payment',o.id]),
      error: o => this.toastService.showErrorToast("Product Out Of Stock!")
    })
    
  }

  deleteItem(cartId: number,item: CartItem){
    this.cartService.removeItemFromCart(cartId,{productId: item.productId, quantity: item.quantity}).subscribe();
  }
}
