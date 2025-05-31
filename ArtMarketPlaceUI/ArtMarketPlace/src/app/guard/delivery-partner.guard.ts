import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { inject } from '@angular/core';

export const deliveryPartnerGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const userService = inject(UserService);
  const role = userService.getUserTokenInfo().role;
  if(role != 'Delivery') {
    router.navigate(['**'])
    return false;
  }
  return true;
};
