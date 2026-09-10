import { HttpErrorResponse, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { BehaviorSubject, catchError, filter, switchMap, take, throwError } from 'rxjs';
import { AuthService } from '../auth/auth-service';
import { Router } from '@angular/router';
import { inject } from '@angular/core';

//Block constant refreshing
let isRefreshing = false;
const refreshTokenSubject = new BehaviorSubject<string | null>(null);

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {

  const authService = inject(AuthService);
  const router = inject(Router);


  //Try to add access token to request
  let authReq = req;
  const token = authService.getToken();

  if(token)
  {
    authReq = addTokenHeader(req, token);
  }


  //Clone request with refresh token
  if(req.url.includes('/api/auth/refresh'))
  {
    authReq = authReq.clone({ withCredentials: true });
  }


  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) =>{

      //If user is unauthorized and is not refreshing token
      if(error.status === 401 && !authReq.url.includes('/api/auth/refresh'))
      {
          return handle401Error(authReq, next, authService, router);
      }

      return throwError(() => error);
    })
  )
};


//Clone request with access token
const addTokenHeader = (request: HttpRequest<unknown>, token: string) => {
  return request.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });
};



//Handle 401 error
const handle401Error = (request: HttpRequest<unknown>, next: HttpHandlerFn, authService: AuthService, router: Router)=>
{
  if(!isRefreshing)
  {
    isRefreshing = true;
    //Reset subject
    refreshTokenSubject.next(null);

    //Call refresh endpoint
    return authService.refreshToken().pipe(
      switchMap((response: any)=>{
        isRefreshing = false;

        //Save new access token in localstorage
        const newAccessToken = response.accessToken;
        authService.setToken(newAccessToken);

        //Unblock waiting requests
        refreshTokenSubject.next(newAccessToken);

        //Renew original request that caused 401
        return next(addTokenHeader(request, newAccessToken));
      }),
      catchError((error) => {
        //Refresh token expired
        isRefreshing = false;
        authService.logout();
        router.navigate(['/login']); 
        return throwError(() => error);
      })
    )
  }
  else{
    return refreshTokenSubject.pipe(
      filter(token => token !== null),
      take(1),
      switchMap(token => {
        //Send request again with token
        return next(addTokenHeader(request, token!));
      })
    );
  }
}