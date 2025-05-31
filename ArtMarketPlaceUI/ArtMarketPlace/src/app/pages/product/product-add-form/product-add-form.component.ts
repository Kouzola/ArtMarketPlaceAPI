import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { ProductService } from '../../../services/product.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FileImageValidators } from '../../../shared/fileImageValidators';
import { CategoryService } from '../../../services/category.service';
import { UserService } from '../../../services/user.service';
import { ToastService } from '../../../services/toast.service';


@Component({
  selector: 'app-product-add-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './product-add-form.component.html',
  styleUrl: './product-add-form.component.css'
})
export class ProductAddFormComponent implements OnInit{
  private readonly router = inject(Router);
  addProductForm: FormGroup;
  formBuilder = inject(FormBuilder);
  productService = inject(ProductService);
  categoryService = inject(CategoryService);
  userService = inject(UserService);
  toastService = inject(ToastService);
  categories$ = this.categoryService.categories$;

  constructor(){
    this.addProductForm = this.formBuilder.group({
      name:['',Validators.required],
      description:['',Validators.required],
      price:[0,[Validators.required,Validators.pattern(/^\d+(,\d{1,2})?$/)]],
      imageFile:[null,[Validators.required,
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
  }

  addProduct(){
    const formData = new FormData();
    Object.entries(this.addProductForm.value).forEach(([key, value]) => {
      if(key !== 'image') formData.append(key, value as string);
      else formData.append(key, value as File);
    });


    this.productService.addProduct(formData).subscribe({
      next: () => {
        this.toastService.showSuccesToast("Product Added!");
        this.router.navigate(['/home/myProducts']);
      },
      error: () => this.toastService.showErrorToast("Error while adding the product!")
    });
  }

  onFileChanged(event: Event){
    const input = event.target as HTMLInputElement;

    if (input?.files?.length) {
      const file = input.files[0];
      this.addProductForm.get('imageFile')?.setValue(file);
      this.addProductForm?.updateValueAndValidity();
    }
  }

  onFileFormTouched(){
    this.addProductForm?.get('imageFile')?.markAsTouched();
  }

}
