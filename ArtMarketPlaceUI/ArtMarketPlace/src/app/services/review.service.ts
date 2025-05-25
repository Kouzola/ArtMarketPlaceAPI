import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, tap } from 'rxjs';
import { Review } from '../model/review.model';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  apiUrl = environment.apiUrl;
  private reviews = new BehaviorSubject<Review[] | null>(null);
  public reviews$ = this.reviews.asObservable();

  constructor(private http: HttpClient) { }

  URL = this.apiUrl + '/api/Review'

  getAllReviewOfAProduct(productId: number){
    const finalUrl = this.URL + `/${productId}`;
    return this.http.get<Review[]>(finalUrl).pipe(
      tap(r => this.reviews.next(r))
    );
  }

  getReviewById(id: number){
    const finalUrl = this.URL + `/by-Id/${id}`;
    return this.http.get<Review>(finalUrl);
  }

  addReview(review: Review){
    return this.http.post<Review>(this.URL,review);
  }

  updateReview(review: Review){
    const finalUrl = this.URL + `/${review.id}`;
    return this.http.put<Review>(finalUrl,review);
  }

  deleteReview(id: number){
    const finalUrl = this.URL + `/${id}`;
    return this.http.delete<Review>(finalUrl);
  }

}
