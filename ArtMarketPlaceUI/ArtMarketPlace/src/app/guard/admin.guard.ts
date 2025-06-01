import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
    const userService = inject(UserService);
    const role = userService.getUserTokenInfo().role;
    if(role != 'Admin') {
      router.navigate(['**'])
      return false;
    }
    return true;
};
