import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ReviewService } from '../../../services/review.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { ToastService } from '../../../services/toast.service';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-reviews',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './reviews.component.html',
  styleUrl: './reviews.component.css'
})
export class ReviewsComponent implements OnInit{
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  reviewService = inject(ReviewService);
  reviews$ = this.reviewService.reviews$;
  userService = inject(UserService);
  toastService = inject(ToastService);
  responseReviewForm: FormGroup;
  formBuilder = inject(FormBuilder);

  constructor(){
    this.responseReviewForm = this.formBuilder.group({
      answer: ["",Validators.required]
    })
  }
  
  ngOnInit(): void {
    this.loadPage();
  }



  loadPage(){
    this.route.paramMap.subscribe( params => {
      this.reviewService.getAllReviewOfAProduct(Number(params.get('id')!)).subscribe();
    })
  }

  respondToReview(id:number){
    this.reviewService.respondToAReview(id, this.responseReviewForm.value).subscribe({
      next: () => {
        this.toastService.showSuccesToast("Review Status Updated!");
        this.toggleViewResponseView(id);
        this.loadPage();
      }
      });
  }

  toggleViewResponseView(reviewId: number){
    const element = document.getElementById(`review-textarea-${reviewId}`);
    if(element?.style.display === 'none') element!.style.display = 'block';
    else element!.style.display = 'none';
  }

}
