import { inject } from '@angular/core/primitives/di';
import { CanActivateFn, Router } from '@angular/router';
import { LoginService } from '../auth/login-service';

export const guestGuard: CanActivateFn = (route, state) => {

  const router = inject(Router);
  const loginService = inject(LoginService);

  let isLoggedIn = loginService.isLoggedIn();
  
  if(isLoggedIn)
  {
    router.navigate(['/main-page']);
    return false;
  }

  return true;
};
