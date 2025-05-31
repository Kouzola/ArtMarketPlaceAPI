import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';

export const dashboardGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
    const userService = inject(UserService);
    const role = userService.getUserTokenInfo().role;
    if(role === 'Customer'){
      router.navigate(['/home/products']);
      return false;
    }
    return true;
};
