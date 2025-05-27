import { Component, inject, OnInit } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { ProductService } from '../../../services/product.service';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

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
  

  ngOnInit(): void {
    this.refreshProductList();
  }
  

  refreshProductList(){
    const artisanId = this.userService.getUserTokenInfo().id;
    this.productService.getProductsByArtisan(artisanId).subscribe();
  }
  
}
