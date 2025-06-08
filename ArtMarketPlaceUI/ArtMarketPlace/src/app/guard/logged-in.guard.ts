import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const loggedInGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = sessionStorage.getItem('jwt');
    if (token) {
      router.navigate(['/home']);
      return false;
    }
    return true;
};
