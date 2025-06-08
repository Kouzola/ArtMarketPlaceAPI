import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';
import { Shipment, ShipmentStatus } from '../model/shipment.model';

@Injectable({
  providedIn: 'root'
})
export class ShipmentService {
   apiUrl = environment.apiUrl;

  private shipments = new BehaviorSubject<Shipment[] | null>([]);
  public shipments$ = this.shipments.asObservable();

  private shipment = new BehaviorSubject<Shipment | null>(null);
  public shipment$ = this.shipment.asObservable();

  constructor(private http: HttpClient) { }

  URL = this.apiUrl + '/api/Shipment'

  getShipmentByOrderAndProduct(orderId: number, productId: number){
    const finalUrl = this.URL + `/${orderId}?productId=${productId}`
    return this.http.get<Shipment[]>(finalUrl).pipe(
      tap((s) => this.shipments.next(s))
    );
  }

  getAllShipmentOfAnOrder(orderId: number){
    const finalUrl = this.URL + `/by-order/${orderId}`
    return this.http.get<Shipment[]>(finalUrl).pipe(
      tap((s) => this.shipments.next(s))
    )
  }

  getAllShipmentOfPartnerId(deliveryPartnerId: number){
    const finalUrl = this.URL + `/by-delivery/${deliveryPartnerId}`
    return this.http.get<Shipment[]>(finalUrl).pipe(
      tap((s) => this.shipments.next(s))
    )
  }

  getShipmentById(id: number){
    const finalUrl = this.URL + `/${id}`
    return this.http.get<Shipment>(finalUrl).pipe(
      tap((s) => this.shipment.next(s))
    )
  }

  updateShipmentDeliveryStatus(shipmentId: number, status: ShipmentStatus){
    const finalUrl = this.URL + `/status/${shipmentId}`
    return this.http.put<Shipment>(finalUrl,status,
      {
        headers: { 'Content-Type': 'application/json' }
      }
    );
  }

}
