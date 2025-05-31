import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomizationService } from '../../services/customization.service';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-customization',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './customization.component.html',
  styleUrl: './customization.component.css'
})
export class CustomizationComponent implements OnInit{
  
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  customizationService = inject(CustomizationService);
  toastService = inject(ToastService);
  customization$ = this.customizationService.customizations$;
  productId = 0;

  ngOnInit(): void {
    this.refreshCustomizationList();
  }

  addCustomization(productId: number){
    this.router.navigate([`/home/customizations/${productId}/add`]);
  }

  editCustomization(customizationId: number){
    this.router.navigate([`/home/customizations/${this.productId}/edit`,customizationId]);
  }

  deleteCustomization(id: number){
    this.customizationService.deleteCustomization(id).subscribe({
      next: () => {
        this.toastService.showSuccesToast("Customization Deleted!");
        this.refreshCustomizationList();
      },
      error: () => this.toastService.showErrorToast("Customization not deleted!")
    })
  }

  refreshCustomizationList(){
    this.route.paramMap.subscribe( params => {
      this.customizationService.getCustomizationByProduct(Number(params.get('productId')!)).subscribe(),
      this.productId = Number(params.get('productId')!);
    })
  }
}
