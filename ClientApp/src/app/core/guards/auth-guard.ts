import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../auth/auth-service'
import { map, take } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const authService = inject(AuthService);

  return authService.isLoggedIn$.pipe(
    take(1),
    map(isLoggedIn =>{
      if(!isLoggedIn)
      {
        //User is not authenticated
        router.navigate(['/login']);
        return false;
      }

      return true
    })
  );
  
};
