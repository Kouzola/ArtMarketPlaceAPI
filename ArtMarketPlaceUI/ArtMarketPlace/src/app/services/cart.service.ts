import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, of, tap } from 'rxjs';
import { Cart, CartItem } from '../model/cart.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  
  apiUrl = environment.apiUrl;
  private cart = new BehaviorSubject<Cart | null>(null);
  public cart$ = this.cart.asObservable();

  constructor(private http: HttpClient) { }

  URL = this.apiUrl + '/api/Cart';

  GetCustomerCart(customerId: number){
    const finalUrl = this.URL + `/${customerId}`;
    return this.http.get<Cart>(finalUrl).pipe(
      catchError(err => {
      console.error('Erreur panier :', err);
      return of(null);
    }),
      tap(c => this.cart.next(c))
    );
  }

  AddItemToCart(item: {userId: number,productId: number,quantity: number}){
    return this.http.post<Cart>(this.URL,item);
  }

  removeItemFromCart(cartId: number, item: {productId: number,quantity: number}){
    const finalUrl = this.URL + `/${cartId}`;
    return this.http.put<Cart>(finalUrl, item).pipe(
      tap(c => this.cart.next(c))
    );
  }

  deleteCart(cartId: number){
    const finalUrl = this.URL + `/${cartId}`;
    return this.http.delete(finalUrl);
  }
}
