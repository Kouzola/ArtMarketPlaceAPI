import { Component, inject } from '@angular/core';
import { CategoryService } from '../../../services/category.service';
import { Router } from '@angular/router';
import { ToastService } from '../../../services/toast.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent {
  private readonly router = inject(Router);
  categoryService = inject(CategoryService);
  categories$ = this.categoryService.categories$;
  toastService = inject(ToastService);
  

  ngOnInit(): void {
    this.refreshCategoryList();
  }
  

  refreshCategoryList(){
    this.categoryService.getAllCategories().subscribe();
  }

  deleteCategory(categoryId: number){
    this.categoryService.deleteCategory(categoryId).subscribe({
      next: () => {
        this.toastService.showSuccesToast("Category Deleted!");
        this.refreshCategoryList();
      },
      error: () => this.toastService.showErrorToast("The category cannot be deleted!")
    }
    );
    
  }
  

  editCategory(categoryId: number){
    this.router.navigate(['/home/categories/edit',categoryId])
  }
}
