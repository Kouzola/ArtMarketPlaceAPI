import { Component, inject, input } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-side-bar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.css'
})
export class SideBarComponent {
  router = inject(Router);
  userName = input<string>();
  role = input<'Customer' | 'Artisan' | 'Delivery' | 'Admin'>('Customer');
  //RouterLink Variable + Class de logo + les textes
  actualRole = 'Customer';
  rolesLayout: Record<'Customer' | 'Artisan' | 'Delivery' | 'Admin', { route: string; title: string; logo: string }[]>= {
    Customer: [
      {route: '/home/products', title: 'Products', logo: 'bi bi-shop'},
      {route: '/home/orders', title: 'My Orders', logo: 'bi bi-bag'},
      {route: '/home/inquiries', title: 'My Inquiries', logo: 'bi bi-question-circle'},
      {route: '/home/cart', title: 'My Cart', logo: 'bi bi-cart'}
    ],
    Artisan: [
      {route: '/home/dashboard', title: 'My Dashboard', logo: 'bi bi-info-circle'},
      {route: '/home/myProducts', title: 'My Products', logo: 'bi bi-shop'},
      {route: '/home/orders', title: 'My Orders', logo: 'bi bi-bag'},
    ],
    Delivery: [
      {route: '/home/dashboard', title: 'My Dashboard', logo: 'bi bi-info-circle'},
      {route: '/home/shipments', title: 'My Shipments', logo: 'bi bi-truck'},
    ],
    Admin: [
      {route: '/home/productsList', title: 'Products List', logo: 'bi bi-list-ol'},
      {route: '/home/userList', title: 'User Lists', logo: 'bi bi-people'},
    ]
  };

  onSignOut(){
    sessionStorage.removeItem('jwt');
    this.router.navigate(['/login']);
  }
}
