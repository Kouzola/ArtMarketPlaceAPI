import { Component, inject, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { ProductService } from '../../services/product.service';
import { ReviewService } from '../../services/review.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CartService } from '../../services/cart.service';
import { UserService } from '../../services/user.service';
import { CustomizationService } from '../../services/customization.service';
import { ToastService } from '../../services/toast.service';
import { OrderService } from '../../services/order.service';
import { concatMap, map, mergeMap, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css',
})
export class ProductComponent implements OnInit {
  apiUrl = environment.apiUrl;
  productService = inject(ProductService);
  reviewService = inject(ReviewService);
  cartService = inject(CartService);
  userService = inject(UserService);
  orderService = inject(OrderService);
  customizationService = inject(CustomizationService);
  toastService = inject(ToastService);
  product$ = this.productService.product$;
  reviews$ = this.reviewService.reviews$;
  customizations$ = this.customizationService.customizations$;
  private readonly route = inject(ActivatedRoute);
  selectedQuantity: number = 1;
  selectedCustomizationId: number = 0;
  customizationPrice: number = 0;
  isAlreadyReviewed = false;
  isOrderByCustomer = false;
  reviewForm: FormGroup;
  showReviewForm = false;
  formBuilder = inject(FormBuilder);
  productId = 0;
  constructor() {
    this.reviewForm = this.formBuilder.group({
      customerId: [this.userService.getUserTokenInfo().id],
      title: ['', Validators.required],
      description: ['', Validators.required],
      score: [0, [Validators.required, Validators.min(0), Validators.max(5)]],
      productId: [0],
    });
  }

  index: number[] = [];

  ngOnInit(): void {
    for (let i = 1; i <= 10; i++) {
      this.index.push(i);
    }
    this.route.paramMap.subscribe((params) => {
      this.productService
        .getProductById(Number(params.get('productId')!))
        .pipe(
          concatMap((product) =>
            this.orderService
              .getAllCustomerOrder(this.userService.getUserTokenInfo().id)
              .pipe(
                tap(
                  (orders) =>
                    (this.isOrderByCustomer = orders.some((order) =>
                      order.productsOrderedInfo.some(
                        (productInfo) =>
                          productInfo.reference == product.reference
                      )
                    ))
                )
              )
          )
        )
        .subscribe(),
        this.getProductsReview(Number(params.get('productId')!)),
        (this.productId = Number(params.get('productId')!)),
        this.reviewForm.get('productId')?.setValue(this.productId);
    });
  }

  getProductsReview(productId: number) {
    this.reviewService.getAllReviewOfAProduct(productId).subscribe({
      next: (s) =>
        (this.isAlreadyReviewed = s.some(
          (review) =>
            review.customerId == this.userService.getUserTokenInfo().id
        )),
    }),
      this.customizationService
        .getCustomizationByProduct(productId)
        .subscribe();
  }

  addToCart(productId: number) {
    this.cartService
      .AddItemToCart({
        userId: this.userService.getUserTokenInfo().id,
        productId: productId,
        quantity: this.selectedQuantity,
        customizationId: this.selectedCustomizationId,
      })
      .subscribe({
        next: (v) =>
          this.toastService.showSuccesToast('Product Added to Cart!'),
      });
  }

  onCustomizationChanged() {
    if (this.selectedCustomizationId != 0) {
      this.customizationService
        .getCustomizationById(this.selectedCustomizationId)
        .subscribe({
          next: (s) => (this.customizationPrice = s.price),
        });
    } else {
      this.customizationPrice = 0;
    }
  }

  addReview() {
    this.reviewService.addReview(this.reviewForm.value).subscribe({
      next: () => {
        this.toastService.showSuccesToast('Review Added!');
        this.getProductsReview(this.productId);
        this.showReviewForm = false;
      },
    });
  }

  onAddReviewButtonClicked() {
    this.showReviewForm = true;
  }
}
