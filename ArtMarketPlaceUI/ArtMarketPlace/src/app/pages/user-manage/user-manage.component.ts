import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-user-manage',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './user-manage.component.html',
  styleUrl: './user-manage.component.css'
})
export class UserManageComponent implements OnInit{
  private readonly router = inject(Router);
  updateForm: FormGroup;
  formBuilder = inject(FormBuilder);
  userService = inject(UserService);
  toastService = inject(ToastService);
  changePassword: boolean = false;
  user$ = this.userService.user$;

  constructor () {
      this.updateForm = this.formBuilder.group({
        userName: ['',Validators.required],
        firstName: ['',Validators.required],
        lastName: ['',Validators.required],
        email: ['',[Validators.required, Validators.email]],
        street: ['',Validators.required],
        city: ['',Validators.required],
        postalCode: ['',Validators.required],
        country: ['',Validators.required],
      })
  }
  ngOnInit(): void {
    const userId = this.userService.getUserTokenInfo().id;
    this.userService.getUserbyId(userId).subscribe();
    this.loadUser();
  }

  onUpdateUserData(id: number){
    const userLoginDate = this.updateForm.value;
    this.userService.updateUserInfo(id,userLoginDate).subscribe({
      next: (v) => {
        this.reloadPage();
        this.toastService.showSuccesToast("User Data updated!");
      },
      error: (e) => {
        if(e.error){
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

  onChangePasswordClicked(){
    this.updateForm.addControl(
    'password',
    new FormControl('', [
      Validators.required,
      Validators.minLength(10),
      Validators.pattern('.*[A-Z].*'),
      Validators.pattern('.*[a-z].*'),
      Validators.pattern('.*[0-9].*'),
      Validators.pattern('.*[^\w\d\s].*'),
    ])
  );
    this.changePassword = true;
  }

  loadUser(){
    this.user$.subscribe((user) => {
      console.log(user);
      if(user){
        this.updateForm.patchValue({
          userName: user.userName,
          firstName: user.firstName,
          lastName: user.lastName,
          email: user.email,
          street: user.address!.street,
          city: user.address!.city,
          postalCode: user.address!.postalCode,
          country: user.address!.country,
        })
      }});
  }

  reloadPage(){
    const currentUrl = this.router.url;
    //Aller vers / sans changer l'url de la barre du navigateur pour après revenir sur l'url actuel pour trigger un reload de component
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
    this.router.navigate([currentUrl]);
  });
  }
}
