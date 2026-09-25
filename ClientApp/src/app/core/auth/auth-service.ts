import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { BehaviorSubject, map, Observable, switchMap, tap } from 'rxjs';
import { LoginRequest, LoginResponse } from '../models/auth/login-models';
import { CurrentUser } from '../models/user/currentUser';

@Injectable({
  providedIn: 'root',
})


export class AuthService {

  private http = inject(HttpClient);
  private readonly apiUrl = environment.apiUrl;
  private readonly TOKEN_KEY = 'access_token';


  //User data
  private currentUserSubject = new BehaviorSubject<CurrentUser | null>(null);

  //Expose to component
  public currentUserSubject$ = this.currentUserSubject.asObservable();
  

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
      }),

      switchMap(response => this.fetchCurrentUser().pipe(
          map(() => response)
      ))
    );
  }


  //Get user profile data
  fetchCurrentUser(): Observable<CurrentUser>
  {
    return this.http.get<CurrentUser>(`${this.apiUrl}/users/me`).pipe(
      tap((user)=>{
        //Save user data
        this.currentUserSubject.next(user);
      })
    )
  }


  //Refresh user access token
  refreshToken(): Observable<{accessToken: string}>
  {
    return this.http.post<{accessToken: string}>(`${this.apiUrl}/auth/refresh`, {}, {withCredentials: true})
    .pipe(
      tap(response =>{
        this.setToken(response.accessToken)
        this.loggedInSubject.next(true);
      })
    );
  }

  

  //User logout
  logout(): void
  {
    localStorage.removeItem(this.TOKEN_KEY);
    this.loggedInSubject.next(false)
    
    //Clear user data after logout
    this.currentUserSubject.next(null);
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
