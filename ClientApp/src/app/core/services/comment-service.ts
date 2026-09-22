import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CommentService 
{
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;

  
  //Get meeting comments
  getComments(meetingId: number): Observable<Comment[]>
  {
    return this.http.get<Comment[]>(`${this.apiUrl}/meetings/${meetingId}/comments`);
  }
}
