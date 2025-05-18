import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { userRegister } from '../../model/userRegister.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  private readonly router = inject(Router);
  registerForm: FormGroup;
  formBuilder = inject(FormBuilder);
  authService = inject(AuthService);
  
  constructor () {
    this.registerForm = this.formBuilder.group({
      userName: ['',Validators.required],
      password: ['',[
        Validators.required,
        Validators.minLength(10),
        Validators.pattern('.*[A-Z].*'),
        Validators.pattern('.*[a-z].*'),
        Validators.pattern(".*[0-9].*"),
        Validators.pattern('.*[^\w\d\s].*'),
      ]],
      firstName: ['',Validators.required],
      lastName: ['',Validators.required],
      email: ['',[Validators.required, Validators.email]],
      street: ['',Validators.required],
      city: ['',Validators.required],
      postalCode: ['',Validators.required],
      country: ['',Validators.required],
      role: [null,Validators.required],
    })
  }
  ngOnInit(): void {
    this.registerForm.markAsPristine();
    this.registerForm.markAsUntouched();
  }

  onSignUp(){
    const userLoginDate = this.registerForm.value;
    this.authService.register(userLoginDate).subscribe({
      next: (v) => {
        this.router.navigate(['/login']);
      }, //TODO: TOAST success
      error: (e) => {
        if(e.error.errors){
          const errorMessages = Object.entries(e.error.errors);
          if(errorMessages.length > 0){
            const messages = errorMessages.map(([subject,messages]) =>`${messages}`)
              .join('\n');

            alert(messages);
          }
        }       
      const errorMessage2 = e.error.exceptionMessage;
      if(errorMessage2) alert(errorMessage2);
      },
    });
  }
}
