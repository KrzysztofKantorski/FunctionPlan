import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import {MatSnackBar} from '@angular/material/snack-bar';
import { catchError, throwError } from 'rxjs';


export const errorInterceptor: HttpInterceptorFn = (req, next) => {

  const router = inject(Router);
  const snackBar = inject(MatSnackBar);


  return next(req).pipe(
    catchError((error: HttpErrorResponse) => 
    {

      if (error.status === 401) 
      {
        snackBar.open('Login again.', 'Close', { panelClass: 'error-snackbar' });
        router.navigate(['/login']);
      } 

      else if (error.status === 400) 
      {
        let errorMessage = 'Incorrect request data';

        if (error.error && error.error.errors) 
        {
          //Get errors from fluent validation
          const validationMessages = Object.values(error.error.errors).flat() as string[];

          if (validationMessages.length > 0) 
          {
            errorMessage = validationMessages.join('\n'); 
          }

        }

        else if (typeof error.error === 'string') 
        {
          errorMessage = error.error;
        }

        snackBar.open(errorMessage, 'Close', { panelClass: 'error-snackbar', duration:5000  });
      }

      else if (error.status === 403) 
      {
        snackBar.open('You are not authorized to perform this action.', 'Close', { panelClass: 'error-snackbar' });
      } 

      else if (error.status === 429) 
      {
        snackBar.open('Request limit has been exceeded.', 'Close', { panelClass: 'error-snackbar' });
      } 

      else if (error.status >= 500) 
      {
        snackBar.open('Server error occurred.', 'Close', { panelClass: 'error-snackbar' });
      }


      return throwError(() => error)
    })
  );
};
