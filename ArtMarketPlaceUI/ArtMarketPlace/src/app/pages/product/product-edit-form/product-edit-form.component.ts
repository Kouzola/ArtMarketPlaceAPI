import { Component, inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../../services/category.service';
import { ProductService } from '../../../services/product.service';
import { ToastService } from '../../../services/toast.service';
import { UserService } from '../../../services/user.service';
import { FileImageValidators } from '../../../shared/fileImageValidators';
import { CommonModule } from '@angular/common';
import { combineLatest } from 'rxjs';
import { User } from '../../../model/user.model';

@Component({
  selector: 'app-product-edit-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './product-edit-form.component.html',
  styleUrl: './product-edit-form.component.css'
})
export class ProductEditFormComponent {
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  editProductForm: FormGroup;
  formBuilder = inject(FormBuilder);
  productService = inject(ProductService);
  categoryService = inject(CategoryService);
  userService = inject(UserService);
  toastService = inject(ToastService);
  categories$ = this.categoryService.categories$;
  product$ = this.productService.product$;

  constructor(){
    this.editProductForm = this.formBuilder.group({
      name:['',Validators.required],
      description:['',Validators.required],
      price:['',[Validators.required,Validators.pattern(/^\d+(,\d{1,2})?$/)]],
      imageFile:[null,[
        FileImageValidators()
      ]],
      stock:[0,Validators.required],
      categoryId:[null,Validators.required],
      available:[false],
      artisanId:[this.userService.getUserTokenInfo().id]
    })
  }

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe();
    this.route.paramMap.subscribe(params => {
      this.productService.getProductById(Number(params.get('productId')!)).subscribe()
    });
    this.loadProduct();
  }

  editProduct(productId: number){
    const formData = new FormData();
    Object.entries(this.editProductForm.value).forEach(([key, value]) => {
      if(key !== 'image') formData.append(key, value as string);
      else formData.append(key, value as File);
    });
    formData.append('id',productId.toString());


    this.productService.updateProduct(formData).subscribe({
      next: () => {
        this.toastService.showSuccesToast("Product Updated!");
        this.router.navigate(['/home/myProducts']);
      },
      error: () => this.toastService.showErrorToast("Error while editing the product!")
    });
  }

  onFileChanged(event: Event){
    const input = event.target as HTMLInputElement;

    if (input?.files?.length) {
      const file = input.files[0];
      this.editProductForm.get('imageFile')?.setValue(file);
      this.editProductForm?.updateValueAndValidity();
    }
  }

  onFileFormTouched(){
    this.editProductForm?.get('imageFile')?.markAsTouched();
  }

  loadProduct(){
    combineLatest([this.product$, this.categories$]).subscribe(([product, categories]) => {

    if (product) {
      const category = categories!.find(cat => cat.name === product.category);
      const artisan = product!.artisan as User;

    this.product$.subscribe((product) => {
        this.editProductForm.patchValue({
          name: product!.name,
          description: product!.description,
          price: product!.price.toString().replace('.',','),
          stock: product!.stock,
          categoryId: category!.id,
          artisanId: artisan.id,
          available: product!.available,
        })});

    }
    });
  }
}
