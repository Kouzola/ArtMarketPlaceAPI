import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, tap } from 'rxjs';
import { Order, ShippingOption } from '../model/order.model';
import { PaymentDetail } from '../model/paymentDetail.model';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  apiUrl = environment.apiUrl;
  private orders = new BehaviorSubject<Order[] | null>([]);
  public orders$ = this.orders.asObservable();

  private order = new BehaviorSubject<Order | null>(null);
  public order$ = this.order.asObservable();

  constructor(private http: HttpClient) {}

  URL = this.apiUrl + '/api/Order';

  getAllCustomerOrder(customerId: number) {
    const finalUrl = this.URL + `/by-CustomerId/${customerId}`;
    return this.http
      .get<Order[]>(finalUrl)
      .pipe(tap((o) => this.orders.next(o)));
  }

  getAllArtisanOrder(artisanId: number) {
    const finalUrl = this.URL + `/by-ArtisanId/${artisanId}`;
    return this.http
      .get<Order[]>(finalUrl)
      .pipe(tap((o) => this.orders.next(o)));
  }

  getOrderById(orderId: number) {
    const finalUrl = this.URL + `/${orderId}`;
    return this.http.get<Order>(finalUrl).pipe(tap((o) => this.order.next(o)));
  }

  getOrderTotalPrice(orderId: number) {
    const finalUrl = this.URL + `/price/${orderId}`;
    return this.http.get<number>(finalUrl);
  }

  createOrderFromCart(orderInfo: {
    cartId: number;
    customerId: number;
    shippingOption: ShippingOption;
  }) {
    return this.http.post<Order>(this.URL, orderInfo);
  }

  payOrder(orderId: number, paymentDetail: PaymentDetail) {
    const finalUrl = this.URL + `/pay/${orderId}`;
    return this.http.put<Order>(finalUrl, paymentDetail);
  }

  shipOrder(
    orderId: number,
    orderInfo: { deliveryPartnerId: number; artisanId: number }
  ) {
    const finalUrl = this.URL + `/ship/${orderId}`;
    return this.http.put(finalUrl, orderInfo);
  }

  validateProductInAOrder(orderId: number, artisanId: number) {
    const finalUrl = this.URL + `/productValidate/${orderId}`;
    return this.http.put(finalUrl, artisanId, {
      headers: { 'Content-Type': 'application/json' },
    });
  }

  cancelOrder(orderId: number) {
    const finalUrl = this.URL + `/${orderId}`;
    return this.http.delete(finalUrl);
  }
}
