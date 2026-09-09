import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { LoginService } from '../auth/login-service';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const loginService = inject(LoginService);

  let isLoggedIn = loginService.isLoggedIn();
  if(!isLoggedIn)
  {
    router.navigate(['/login']);
    return false;
  }
  return true;
  
};
