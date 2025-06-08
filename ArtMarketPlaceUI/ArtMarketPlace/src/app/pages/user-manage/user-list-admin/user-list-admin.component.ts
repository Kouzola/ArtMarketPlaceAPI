import { Component, inject, OnInit } from '@angular/core';
import { AdminService } from '../../../services/admin.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserRolePipe } from "../../../shared/user-role.pipe";
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-user-list-admin',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, UserRolePipe],
  templateUrl: './user-list-admin.component.html',
  styleUrl: './user-list-admin.component.css'
})
export class UserListAdminComponent implements OnInit{
  private readonly router = inject(Router);
  adminService = inject(AdminService);
  toastService = inject(ToastService);
  users$ = this.adminService.users$;

  ngOnInit(): void {
    this.reloadPage();
  }

  reloadPage(){
    this.adminService.getAllUsers().subscribe();
  }

  onUserClicked(userId: number){
    const element = document.getElementById(`user-info-${userId}`);
    if(element?.style.display === 'none') element!.style.display = 'block';
    else element!.style.display = 'none';
  }

  editUser(userId: number){
    this.router.navigate(['/home/userList',userId]);
  }

  deleteUser(userId: number){
    this.adminService.deleteUserById(userId).subscribe({
      next: (s) => {
        this.toastService.showSuccesToast("User Deleted!");
        this.reloadPage();
      },
      error: (e) => this.toastService.showErrorToast("User not deleted!")
    })
  }


  
}
