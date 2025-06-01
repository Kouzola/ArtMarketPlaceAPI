import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, tap } from 'rxjs';
import { Category } from '../model/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  apiUrl = environment.apiUrl;
  private categories = new BehaviorSubject<Category[] | null>([]);
  public categories$ = this.categories.asObservable();

  private category = new BehaviorSubject<Category| null>(null);
  public category$ = this.category.asObservable();

  constructor(private http: HttpClient) { }

  URL = this.apiUrl + '/api/Categories'


  getAllCategories(){
    return this.http.get<Category[]>(this.URL).pipe(
      tap(c => this.categories.next(c))
    );
  }

  getCategoryById(id: number){
    const finalUrl = this.URL + `/${id}`
    return this.http.get<Category>(finalUrl).pipe(
      tap((c) => this.category.next(c))
    )
  }

  addCategory(category: Category){
    return this.http.post<Category>(this.URL,category);
  }

  updateCategory(category: Category){
    const finalUrl = this.URL + `/${category.id}`
    return this.http.put<Category>(finalUrl,category);
  }

  deleteCategory(id: number){
    const finalUrl = this.URL + `/${id}`
    return this.http.delete(finalUrl);
  }



}
