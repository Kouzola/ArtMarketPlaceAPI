import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoryService } from '../../../services/category.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-category-edit-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './category-edit-form.component.html',
  styleUrl: './category-edit-form.component.css'
})
export class CategoryEditFormComponent {
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  categoryService = inject(CategoryService);
  toastService = inject(ToastService);
  editCategoryForm: FormGroup;
  formBuilder = inject(FormBuilder);
  categoryId = 0;
  category$ = this.categoryService.category$;

  constructor(){
    this.editCategoryForm = this.formBuilder.group({
      id:[0,Validators.required],
      name:['',Validators.required],
      description:['',Validators.required],
    });

    this.route.paramMap.subscribe(params => {
      this.editCategoryForm.get('id')?.setValue(Number(params.get('categoryId')!)),
      this.categoryId = Number(params.get('categoryId')!),
      this.categoryService.getCategoryById(this.categoryId).subscribe(),
      this.loadCustomizationData();
    })
  }

  updateCustomization(){
    const productId =  this.editCategoryForm.get('id');
    this.categoryService.updateCategory(this.editCategoryForm.value).subscribe({
      next: () => {
        this.toastService.showSuccesToast("Category Updated!");
        this.router.navigate([`/home/categories`]);
      },
      error: () => this.toastService.showErrorToast("Error while updating the category!")
    })
  }

  loadCustomizationData(){
    this.category$.subscribe((category) => {
      this.editCategoryForm.patchValue({
        id:category?.id,
        name: category?.name,
        description: category?.description,
      })
    })
  }
}
