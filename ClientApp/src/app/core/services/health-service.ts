import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class HealthService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;


  //Server health check
  getHealth(){
    return this.http.get(`${this.apiUrl}/health`)
  }
}
