import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Category } from '../model/category.model';
import { Customization } from '../model/customization.model';

@Injectable({
  providedIn: 'root'
})
export class CustomizationService {
  
  apiUrl = environment.apiUrl;
  private customizations = new BehaviorSubject<Customization[]>([]);
  public customizations$ = this.customizations.asObservable();

  private customization = new BehaviorSubject<Customization | null>(null);
  public customization$ = this.customization.asObservable();

  constructor(private http: HttpClient) { }

  URL = this.apiUrl + '/api/Customizations'

  getAllCustomizations(){
    return this.http.get<Customization[]>(this.URL).pipe(
          tap(c => this.customizations.next(c))
    );
  }

  getCustomizationById(id: number){
    const finalUrl = this.URL + `/${id}`
    return this.http.get<Customization>(finalUrl).pipe(
          tap(c => this.customization.next(c))
    );
  }

  getCustomizationByProduct(productId: number){
    const finalUrl = this.URL + `/by-product/${productId}`
    return this.http.get<Customization[]>(finalUrl).pipe(
          tap(c => this.customizations.next(c))
    );
  }

  addCustomization(customization: Customization){
    return this.http.post<Customization>(this.URL,customization);
  }

  updateCustomization(customization: Customization){
    const finalUrl = this.URL + `/${customization.id}`
    return this.http.put<Customization>(finalUrl,customization);
  }

  deleteCustomization(customizationId: number){
    const finalUrl = this.URL + `/${customizationId}`
    return this.http.delete(finalUrl);
  }


}
