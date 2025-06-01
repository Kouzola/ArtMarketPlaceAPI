import { Component, inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from '../../../services/admin.service';
import { ToastService } from '../../../services/toast.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-manage-admin',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './user-manage-admin.component.html',
  styleUrl: './user-manage-admin.component.css'
})
export class UserManageAdminComponent {
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  updateForm: FormGroup;
  formBuilder = inject(FormBuilder);
  adminService = inject(AdminService);
  changePassword: boolean = false;
  user$ = this.adminService.user$;
  toastService = inject(ToastService);

  constructor () {
      this.updateForm = this.formBuilder.group({
        userName: ['',Validators.required],
        firstName: ['',Validators.required],
        lastName: ['',Validators.required],
        email: ['',[Validators.required, Validators.email]],
        street: ['',Validators.required],
        city: ['',Validators.required],
        role: [0],
        postalCode: ['',Validators.required],
        country: ['',Validators.required],
        active: [true]
      })
  }
  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      this.adminService.getUserbyId(Number.parseInt(params.get('userId')!)).subscribe({
        next: (user) => {
          this.updateForm.get('role')?.setValue(user.role);
          this.loadUser();
        }
      });
    })
    
    
  }

  onUpdateUserData(id: number){
    const userLoginDate = this.updateForm.value;
    this.adminService.updateUserInfo(id,userLoginDate).subscribe({
      next: (v) => {
        this.toastService.showSuccesToast("User Updated!");
        this.router.navigate(['/home/userList']);
      },
      error: (e) => this.toastService.showErrorToast("User Not Updated!"),
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
          role: user.role,
          active: user.active
        })
      }});
  }

}
