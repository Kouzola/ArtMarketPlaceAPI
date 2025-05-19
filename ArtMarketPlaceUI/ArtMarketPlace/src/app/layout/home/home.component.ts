import { Component, inject, input, OnInit } from '@angular/core';
import { SideBarComponent } from "../side-bar/side-bar.component";
import { RouterOutlet } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [SideBarComponent, RouterOutlet],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent{

  userService = inject(UserService);
  userName =  "";
  role!: 'Customer' | 'Artisan' | 'Delivery' | 'Admin';

  ngOnInit(): void {
    const user = this.userService.getUserTokenInfo();
    this.userName = user.name;
    this.role = user.role;
    console.log(this.userName);
  }

}
