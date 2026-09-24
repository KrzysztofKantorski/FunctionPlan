import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { MeetingMediaUrlsResponse } from '../models/media/meetingMediaUrlsResponse';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MediaService 
{
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;


  //Get meeting media info (image url, description, url)
  getMeetingMedia(meetingId: number): Observable<MeetingMediaUrlsResponse[]>
  {
    return this.http.get<MeetingMediaUrlsResponse[]>(`${this.apiUrl}/meetings/${meetingId}/media`)
  }

  //Get media image (as blob)
  getUserImage(meetingId: number, imageId: number): Observable<Blob>
  {
    return this.http.get(`${this.apiUrl}/meetings/${meetingId}/media/${imageId}`, 
    {
      responseType: 'blob'
    })
  }
}
