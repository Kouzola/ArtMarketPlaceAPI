import { Component, inject } from '@angular/core';
import { CategoryService } from '../../../services/category.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastService } from '../../../services/toast.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-category-add-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './category-add-form.component.html',
  styleUrl: './category-add-form.component.css'
})
export class CategoryAddFormComponent {
  private readonly router = inject(Router);
  categoryService = inject(CategoryService);
  toastService = inject(ToastService);
  addCategoryForm: FormGroup;
  formBuilder = inject(FormBuilder);
  productId = 0;

  constructor(){
    this.addCategoryForm = this.formBuilder.group({
      name:['',Validators.required],
      description:['',Validators.required],
    });
  }

  

  addCategory(){
    this.categoryService.addCategory(this.addCategoryForm.value).subscribe({
      next: () => {
        this.toastService.showSuccesToast("Category Added!");
        this.router.navigate(['/home/myProducts']);
      },
      error: () => this.toastService.showErrorToast("Error while adding the category!")
    })
  }
}
