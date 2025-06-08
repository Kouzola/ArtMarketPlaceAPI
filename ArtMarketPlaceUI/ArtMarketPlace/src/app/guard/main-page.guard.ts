import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';

export const mainPageGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const userService = inject(UserService);
  const role = userService.getUserTokenInfo().role;
  if(role === 'Artisan' || role === 'Delivery'){
    router.navigate(['/home/dashboard']);
    return false;
  }
  else if(role === 'Admin'){
    router.navigate(['/home/productsList']);
    return false;
  }
  return true;
};
