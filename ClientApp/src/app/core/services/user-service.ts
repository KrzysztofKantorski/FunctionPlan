import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { UserProfile } from '../models/user-model';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class UserService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;

  //Get user details
  getUserDetails(): Observable<UserProfile>
  {
    return this.http.get<UserProfile>(`${this.apiUrl}/users/me`);
  }
}
