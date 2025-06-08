import { Component, inject, OnInit } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { ProductService } from '../../../services/product.service';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-my-product',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './my-product.component.html',
  styleUrl: './my-product.component.css'
})
export class MyProductComponent implements OnInit{
  
  apiUrl = environment.apiUrl;
  private readonly router = inject(Router);
  productService = inject(ProductService);
  $products = this.productService.products$;
  userService = inject(UserService);
  toastService = inject(ToastService);
  

  ngOnInit(): void {
    this.refreshProductList();
  }
  

  refreshProductList(){
    const artisanId = this.userService.getUserTokenInfo().id;
    this.productService.getProductsByArtisan(artisanId).subscribe();
  }

  deleteProduct(productId: number){
    this.productService.deleteProduct(productId).subscribe({
      next: () => {
        this.toastService.showSuccesToast("Product Deleted!");
        this.refreshProductList();
      },
      error: () => this.toastService.showErrorToast("Product cannot be deleted because it's probably in an order!")
    }
    );
    
  }

  onAddButtonClicked(){
    this.router.navigate(['/home/myProducts/add'])
  }

  onCategoryButtonClicked(){
    this.router.navigate(['/home/categories/add'])
  }

  editProduct(productId: number){
    this.router.navigate(['/home/myProducts/edit',productId]);
  }

  editCustomization(productId: number){
    this.router.navigate(['/home/customizations',productId]);
  }

  getReviews(id: number){
    this.router.navigate(['home/reviews',id]);
  }
  
}
