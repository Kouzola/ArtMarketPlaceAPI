import { Component, inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { CustomizationService } from '../../../services/customization.service';
import { ToastService } from '../../../services/toast.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-customization-edit-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './customization-edit-form.component.html',
  styleUrl: './customization-edit-form.component.css'
})
export class CustomizationEditFormComponent {
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  customizationService = inject(CustomizationService);
  toastService = inject(ToastService);
  editCustomizationForm: FormGroup;
  formBuilder = inject(FormBuilder);
  customizationId = 0;
  customization$ = this.customizationService.customization$;

  constructor(){
    this.editCustomizationForm = this.formBuilder.group({
      id:[0,Validators.required],
      name:['',Validators.required],
      description:['',Validators.required],
      price:[0,[Validators.required]],
      productId:[0,Validators.required],
    });

    this.route.paramMap.subscribe(params => {
      this.editCustomizationForm.get('productId')?.setValue(Number(params.get('productId')!)),
      this.customizationId = Number(params.get('customizationId')!),
      this.customizationService.getCustomizationById(this.customizationId).subscribe(),
      this.loadCustomizationData();
    })
  }

  updateCustomization(){
    const productId =  this.editCustomizationForm.get('productId')!.value;
    this.customizationService.updateCustomization(this.editCustomizationForm.value).subscribe({
      next: () => {
        this.toastService.showSuccesToast("Customization Updated!");
        this.router.navigate([`/home/customizations/${productId}`]);
      },
      error: () => this.toastService.showErrorToast("Error while updating the customization! The price need to be greater than 0!")
    })
  }

  loadCustomizationData(){
    this.customization$.subscribe((customization) => {
      this.editCustomizationForm.patchValue({
        id:customization?.id,
        name: customization?.name,
        description: customization?.description,
        price: customization?.price,
      })
    })
  }
}
