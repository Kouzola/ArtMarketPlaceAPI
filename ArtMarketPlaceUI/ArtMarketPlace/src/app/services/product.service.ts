import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../model/product.model';
import { BehaviorSubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private products =  new BehaviorSubject<Product[]>([]);
  public products$ = this.products.asObservable();

  private product =  new BehaviorSubject<Product | null>(null);
  public product$ = this.product.asObservable();
  
  
  constructor(private http: HttpClient) { }

  URL = 'https://localhost:7166/api/Products'

  getAllProducts(){
    return this.http.get<Product[]>(this.URL).pipe(
      tap(p => this.products.next(p))
    );
  }

  getProductById(id: number){
    const finalUrl = this.URL + `/${id}`;
    return this.http.get<Product>(finalUrl).pipe(
      tap(p => this.product.next(p))
    );
  }

  getProductByReference(reference: string){
    const finalUrl = this.URL + `/by-reference/${reference}`;
    return this.http.get<Product>(finalUrl).pipe(
      tap(p => this.product.next(p))
    );
  }

  getProductsByArtisan(artisanId: number){
    const finalUrl = this.URL + `/by-artisanId/${artisanId}`;
    return this.http.get<Product[]>(finalUrl).pipe(
      tap(p => this.products.next(p))
    );
  }

  getProductByCategory(categoryId: number){
    const finalUrl = this.URL + `/by-categoryId/${categoryId}`;
    return this.http.get<Product[]>(finalUrl).pipe(
      tap(p => this.products.next(p))
    );
  }

  addProduct(product: FormData){
    return this.http.post<Product>(this.URL,product);
  }

  updateProduct(product: FormData){
    const finalUrl = this.URL + `/${product.get('id')}`;
    return this.http.put<Product>(finalUrl,product);
  }

  deleteProduct(id: number){
    const finalUrl = this.URL + `/${id}`;
    return this.http.delete(finalUrl);
  }

}
