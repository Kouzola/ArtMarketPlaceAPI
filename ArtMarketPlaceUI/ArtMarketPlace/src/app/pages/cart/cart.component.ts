import { Component, inject, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CartService } from '../../services/cart.service';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { Cart, CartItem } from '../../model/cart.model';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit{
  
  apiUrl= environment.apiUrl;
  cartService = inject(CartService);
  userService = inject(UserService);
  cart$ = this.cartService.cart$;
  
  
  ngOnInit(): void {
    this.cartService.GetCustomerCart(this.userService.getUserTokenInfo().id).subscribe();
  }

  getTotalPrice(cart: Cart){
    let totalPrice = 0;
    cart.items.forEach(
      item => totalPrice += item.productPrice
    );
    return parseFloat(totalPrice.toFixed(2));
  }

  confirmOrder(){

  }

  deleteItem(cartId: number,item: CartItem){
    this.cartService.removeItemFromCart(cartId,{productId: item.productId, quantity: item.quantity}).subscribe();
  }
}
