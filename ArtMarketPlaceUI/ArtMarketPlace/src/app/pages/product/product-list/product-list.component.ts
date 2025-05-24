import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CardComponent } from "../../../shared/card/card.component";
import { environment } from '../../../../environments/environment';
import { FormsModule } from '@angular/forms';
import { Product } from '../../../model/product.model';
import { map } from 'rxjs';
import { CategoryService } from '../../../services/category.service';
import { Review } from '../../../model/review.model';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, CardComponent,FormsModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit{
  
  apiUrl = environment.apiUrl;
  private readonly router = inject(Router);
  productService = inject(ProductService);
  categoryService = inject(CategoryService);
  products$ = this.productService.products$;
  categories$ = this.categoryService.categories$;
  selectedOption: number = 0;
  search: string = "";
  selectedCategory: string = "All Categories";

  ngOnInit(): void {
    this.refreshProductList();
    this.categoryService.getAllCategories().subscribe();
  }

  onSortingOptionSelected(){
    switch(this.selectedOption){
      case 0:
        this.refreshProductList();
        this.onSearch();
        break;
      case 1:
        this.products$ = this.products$.pipe(map(
          (products: Product[]) => products.sort((a,b) => a.price - b.price)
        ));
        break;
      case 2:
        this.products$ = this.products$.pipe(map(
          (products: Product[]) => products.sort((a,b) => b.price - a.price)
        ));
        break;
      case 3:
      this.products$ = this.products$.pipe(map(
        (products: Product[]) => products.sort((a,b) => new Date(b.updatedAt).getTime() - new Date(a.updatedAt).getTime())
      ));
      break;
    }
  }

  addToCart(){
    
  }

  onCategorySelected(){
    if(this.selectedCategory != 'All Categories'){
      this.products$ = this.products$.pipe(map(
        (products: Product[]) => products.filter((product) => product.category == this.selectedCategory))
      );
    } else {
      this.refreshProductList();
    }  
  }

  onProductClicked(productId: number){
    this.router.navigate(['home/products/view',productId]);
  }

  onSearch(){
    this.products$ = this.products$.pipe(map(
        (products: Product[]) => products.filter((product) => product.name.includes(this.search)))
      );
  }

  refreshProductList(){
    this.products$ = this.productService.getAllProducts()
    this.products$.subscribe(p => console.log(p));
  }

  getProductScore(reviews: Review[]){
    let allReviewScore = 0;
    reviews.forEach(review => {
      allReviewScore += review.score
    });
    const score = allReviewScore / (reviews.length);
    const average = parseFloat(score.toFixed(1));
    return average;
  }
}
