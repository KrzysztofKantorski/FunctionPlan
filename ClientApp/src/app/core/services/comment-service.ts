import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import { CommentMessage } from '../models/comment/comment';
import { CommentRequest } from '../models/comment/commentRequest';
@Injectable({
  providedIn: 'root',
})
export class CommentService 
{
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;


  //Get meeting comments
  getComments(meetingId: number): Observable<CommentMessage[]>
  {
    return this.http.get<CommentMessage[]>(`${this.apiUrl}/meetings/${meetingId}/comments`);
  }

  //Send written comment
  sendComment(meetingId: number, comment: CommentRequest)
  {
    return this.http.post(`${this.apiUrl}/meetings/${meetingId}/comments`, comment);
  }
}
