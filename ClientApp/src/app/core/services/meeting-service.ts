import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { MeetingFilters } from '../models/meeting-filters';
import { Meeting } from '../models/meeting';
import { Observable } from 'rxjs';
import { MeetingDetails } from '../models/meeting-details';

@Injectable({
  providedIn: 'root',
})


export class MeetingService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;




  //Get user meetings with filters (from sidebar and search bar)
  getMeetings(filters: MeetingFilters): Observable <Meeting[]>
  {
    let params = new HttpParams();

    //Send filters if set

    //Search bar
    if(filters.searchTerm)
    {
      params = params.set('SearchTerm', filters.searchTerm)
    }


    //Sort order (by date asc or desc)
    if(filters.sortOrder)
    {
      params = params.set('SortOrder', filters.sortOrder)
    }


    //Status
    if(filters.status!=null && filters.status !=undefined)
    {
      params = params.set('Status', filters.status)
    }



    //Date range - ensure proper format
    if(filters.startDate)
    {
      params = params.set('StartDate', filters.startDate.toISOString())
    }
    if(filters.endDate)
    {
      params = params.set('EndDate', filters.endDate.toISOString())
    }


    //Send request with params
    return this.http.get<Meeting[]>(`${this.apiUrl}/meetings`, {params});
  }



  //Get meeting details
  getMeetingDetails(meetingId: number): Observable<MeetingDetails>
  {
    return this.http.get<MeetingDetails>(`${this.apiUrl}/meetings/${meetingId}`);
  }
}
