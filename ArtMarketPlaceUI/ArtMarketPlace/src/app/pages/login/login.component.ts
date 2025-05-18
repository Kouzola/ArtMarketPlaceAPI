import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import {FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule,} from '@angular/forms'
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  private readonly router = inject(Router);
  loginForm: FormGroup;
  formBuilder = inject(FormBuilder);
  authService = inject(AuthService);

  constructor () {
    this.loginForm = this.formBuilder.group({
      userName: ['',Validators.required],
      password: ['',Validators.required]
    });
  }


  onSignIn(){
    const userLoginData = this.loginForm.value;
    this.authService.login(userLoginData.userName,userLoginData.password).subscribe({
      next: (response) => {
        sessionStorage.setItem('jwt',response.token);
        this.router.navigate(['/home/products']);
      },
      error: (e) => alert("Wrong username or password"),//TODO : ADD TOAST PASSWORD INCORRECT
    });
  }


}
