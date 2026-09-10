import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginRequest, LoginResponse } from '../models/login-models';

@Injectable({
  providedIn: 'root',
})


export class AuthService {

  private http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl;
  private readonly TOKEN_KEY = 'access_token';

  //Check if user is logged in
  private loggedInSubject = new BehaviorSubject<boolean>(this.hasToken());

  //Expose to component
  public isLoggedIn$ = this.loggedInSubject.asObservable();


  //User login
  login(credentials: LoginRequest): Observable<LoginResponse>
  {
    return this.http.post<LoginResponse>(`${this.apiUrl}/auth/login`, credentials, {withCredentials: true})
    .pipe(
      tap(response =>{
        this.setToken(response.accessToken)
        this.loggedInSubject.next(true)
      })
    );
  }


  //Refresh user access token
  refreshToken(): Observable<{accessToken: string}>
  {
    return this.http.post<{accessToken: string}>(`${this.apiUrl}/auth/refresh`, {}, {withCredentials: true})
    .pipe(
      tap(response =>{
        this.setToken(response.accessToken)
      })
    );
  }

  

  //User logout
  logout(): void
  {
    localStorage.removeItem(this.TOKEN_KEY);
    this.loggedInSubject.next(false)
  }

  
  getToken(): string | null
  {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  setToken(token: string): void
  {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  hasToken():boolean
  {
    let token = this.getToken();

    if(token == null)
    {
      return false;
    }

    return true;
  }
}
