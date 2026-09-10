import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../auth/auth-service';
import { map, take } from 'rxjs';

export const guestGuard: CanActivateFn = (route, state) => {

  const router = inject(Router);
  const authService = inject(AuthService);

  return authService.isLoggedIn$
  .pipe(
    //Get value from stream
    take(1), 
    map(isLoggedIn => {
      if (isLoggedIn) {
        router.navigate(['/main-page']);

        //Block access to auth routes
        return false; 
      }
      
      return true; 
    })
  );
};
