import { Component, inject, OnInit } from '@angular/core';
import { CustomizationService } from '../../../services/customization.service';
import { FormGroup, FormBuilder, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../../services/toast.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-customization-add-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './customization-add-form.component.html',
  styleUrl: './customization-add-form.component.css'
})
export class CustomizationAddFormComponent{
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  customizationService = inject(CustomizationService);
  toastService = inject(ToastService);
  addCustomizationForm: FormGroup;
  formBuilder = inject(FormBuilder);

  constructor(){
    this.addCustomizationForm = this.formBuilder.group({
      name:['',Validators.required],
      description:['',Validators.required],
      price:[0,[Validators.required]],
      productId:[0,Validators.required],
    });

    this.route.paramMap.subscribe(params => {
      this.addCustomizationForm.get('productId')?.setValue(Number(params.get('productId')!));
    })
  }

  addCustomization(){
    const productId =  this.addCustomizationForm.get('productId')!.value;
    this.customizationService.addCustomization(this.addCustomizationForm.value).subscribe({
      next: () => {
        this.toastService.showSuccesToast("Customization Added!");
        this.router.navigate([`/home/customizations/${productId}`]);
      },
      error: () => this.toastService.showErrorToast("Error while adding the customization! The price need to be greater than 0!")
    })
  }
}
