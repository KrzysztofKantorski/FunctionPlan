import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CanDeactivateFn } from '@angular/router';
import { HealthService } from '../services/health-service';
import { catchError, map, of } from 'rxjs';

export const serverErrorGuard: CanDeactivateFn<unknown> = () => {
  const snackBar = inject(MatSnackBar);
  const healthService = inject(HealthService);

  //Send request- check if serwer responds
  return healthService.getHealth().pipe(
    map(()=> true),
    catchError(()=>{
      return of(false);
    })
  )
};
