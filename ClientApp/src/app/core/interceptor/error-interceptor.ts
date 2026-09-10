import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import {MatSnackBar} from '@angular/material/snack-bar';
import { catchError, throwError } from 'rxjs';


const extractErrorMessage = (error: HttpErrorResponse, defaultMessage: string): string => {

  if (!error.error) return defaultMessage;

  //Fluent Validation
  if (error.error.errors) 
  {
    const validationMessages = Object.values(error.error.errors).flat() as string[];
    if (validationMessages.length > 0) 
    {
      return validationMessages.join('\n');
    }
  }

  //AppException
  if (error.error.error)
  {
    return error.error.error;
  }
    
  
  //DomainException
  if (error.error.detail) 
  {
    return error.error.detail;
  }
    
  
  if (typeof error.error === 'string') 
  {
    return error.error;
  }
  
  return defaultMessage;
};




export const errorInterceptor: HttpInterceptorFn = (req, next) => {

  const router = inject(Router);
  const snackBar = inject(MatSnackBar);


  return next(req).pipe(
    catchError((error: HttpErrorResponse) => 
    {

      //Default values
      let errorMessage = 'An unexpected error occurred.';
      let duration = 5000;


      if (error.status === 0) {
        errorMessage = 'Cannot connect to server.';
      }

      if (error.status === 401)   
      {
        errorMessage = 'Session expired. Please login again.';
      } 

      else if (error.status === 400) 
      {
        errorMessage = extractErrorMessage(error, 'Incorrect request data');
        duration = 7000;
      }

      else if (error.status === 403) 
      {
        errorMessage = extractErrorMessage(error, 'Action forbidden');
        duration = 7000;
      } 

      else if (error.status === 404) 
      {
        errorMessage = 'Url address not found';
      }

      else if (error.status === 429) 
      {
        errorMessage = 'Request limit has been exceeded.';
      } 

      else if (error.status >= 500) 
      {
        errorMessage = 'Server error occurred.';
      }
      

      //Display error info
      snackBar.open(errorMessage, 'Close', { 
        panelClass: 'error-snackbar', 
        duration: duration 
      });


      return throwError(() => error)
    })
  );
};
