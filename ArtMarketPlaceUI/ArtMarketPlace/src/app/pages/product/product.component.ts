import { Component, inject, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { ProductService } from '../../services/product.service';
import { ReviewService } from '../../services/review.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CartService } from '../../services/cart.service';
import { UserService } from '../../services/user.service';
import { CustomizationService } from '../../services/customization.service';
import { ToastService } from '../../services/toast.service';

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
  cartService = inject(CartService);
  userService = inject(UserService);
  customizationService = inject(CustomizationService);
  toastService = inject(ToastService);
  product$ = this.productService.product$;
  reviews$ = this.reviewService.reviews$;
  customizations$ = this.customizationService.customizations$;
  private readonly route = inject(ActivatedRoute)
  selectedQuantity: number = 1;
  selectedCustomizationId: number = 0;
  customizationPrice: number = 0;

  index: number[] = [];
  



  ngOnInit(): void {
    for(let i=1; i<=10; i++){
    this.index.push(i);
    }
    this.route.paramMap.subscribe(params => {
      this.productService.getProductById(Number(params.get('productId')!)).subscribe(),
      this.reviewService.getAllReviewOfAProduct(Number(params.get('productId')!)).subscribe(),
      this.customizationService.getCustomizationByProduct(Number(params.get('productId')!)).subscribe()
    })

    
  }

  addToCart(productId: number){
    this.cartService.AddItemToCart(
      {userId: this.userService.getUserTokenInfo().id, productId: productId, quantity: this.selectedQuantity, customizationId: this.selectedCustomizationId }
    ).subscribe({
      next: (v) => this.toastService.showSuccesToast("Product Added to Cart!")
    });
  }

  onCustomizationChanged(){
    if(this.selectedCustomizationId != 0){
      this.customizationService.getCustomizationById(this.selectedCustomizationId).subscribe({
        next: (s) => this.customizationPrice = s.price
    })
    } else {
      this.customizationPrice = 0;
    }
    
  }




}
