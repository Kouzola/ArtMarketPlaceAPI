import { Component, inject } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Router } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { ToastService } from '../../../services/toast.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-admin',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './product-admin.component.html',
  styleUrl: './product-admin.component.css'
})
export class ProductAdminComponent {
  apiUrl = environment.apiUrl;
  private readonly router = inject(Router);
  productService = inject(ProductService);
  $products = this.productService.products$;
  toastService = inject(ToastService);
  

  ngOnInit(): void {
    this.refreshProductList();
  }
  

  refreshProductList(){
    this.productService.getAllProducts().subscribe();
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

  onCategoryButtonClicked(){
    this.router.navigate(['/home/categories'])
  }
}
