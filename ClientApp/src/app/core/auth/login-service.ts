import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequest, LoginResponse } from '../models/login-models';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginService 
{

  private apiUrl = 'https://localhost:7206/api';
  private httpClient = inject(HttpClient);

  login(credentials: LoginRequest)
  {
    return this.httpClient.post<LoginResponse>(`${this.apiUrl}/auth/login`, credentials, {withCredentials: true}).pipe(

      tap(response=>
      {
        //Save access token to local storage
        localStorage.setItem('access_token', response.accessToken);
      })
    )
  }

  getToken(): string | null
  {
    return localStorage.getItem('access_token');
  }
}
