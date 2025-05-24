import { Component, inject, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { ProductService } from '../../services/product.service';
import { ReviewService } from '../../services/review.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent implements OnInit{
  apiUrl = environment.apiUrl;
  productService = inject(ProductService);
  reviewService = inject(ReviewService);
  product$ = this.productService.product$;
  private readonly route = inject(ActivatedRoute)
  quantity: number = 1;

  index: number[] = [];
  



  ngOnInit(): void {
    for(let i=1; i<=10; i++){
    this.index.push(i);
    }
    this.route.paramMap.subscribe(params => {
      this.productService.getProductById(Number(params.get('productId')!)).subscribe()
    })
  }

  onQuantityChanged(){

  }


}
