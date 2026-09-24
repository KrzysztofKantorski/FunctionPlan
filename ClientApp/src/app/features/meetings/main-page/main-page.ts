import { Component, inject } from '@angular/core';
import { MainHeader } from '../../../shared/components/main-header/main-header';
import { UserService } from '../../../core/services/user-service';
import { UserProfile } from '../../../core/models/user/user-model';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { NavbarSearch } from '../../../shared/components/nav/navbar-search/navbar-search';
import { Sidebar } from '../../../shared/components/sidebar/sidebar';
import { MeetingCard } from '../../../shared/components/meeting/meeting-card/meeting-card';
import { MeetingFilters } from '../../../core/models/meeting/meeting-filters';
import { MatButtonModule } from '@angular/material/button';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { Meeting } from '../../../core/models/meeting/meeting';
import { MeetingService } from '../../../core/services/meeting-service';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-main-page',
  imports: 
  [ 
    MainHeader, AsyncPipe, Navbar, NavbarBtnGroup, 
    Sidebar, MatButtonModule, NavbarSearch,
    UserMenu, DatePipe, MeetingCard
  ],
  templateUrl: './main-page.html'
})
export class MainPage {

  private userService = inject(UserService);
  private meetingService = inject(MeetingService);

  //Filters state
  private filtersSubject = new BehaviorSubject<MeetingFilters>
  ({
    sortOrder: 'asc'
  });

  userProfile$: Observable<UserProfile> | undefined;
  meetings$: Observable <Meeting[]> | undefined;



  ngOnInit(): void
  {
    this.userProfile$ = this.userService.getUserDetails();

    //Send request when filters change
    this.meetings$ = this.filtersSubject.pipe(

      //Send filters and call service
      switchMap((filters) => this.meetingService.getMeetings(filters))
    )

  }


  //Filters from sidebar
  onFiltersUpdated(sidebarFilters: MeetingFilters)
  {
    //Get current filters from sidebar
    const currentFilters = this.filtersSubject.getValue();

    //Update values
    this.filtersSubject.next
    ({
      ...currentFilters,
      ...sidebarFilters
    })
  }

  //Value from searchbar
  onSearchChanged(searchTerm: string)
  {
    const currentSearch = this.filtersSubject.getValue();

    //Update search text
    this.filtersSubject.next({
      ...currentSearch,
      searchTerm: searchTerm
    });
  }



  onJoinMeeting(meetingId: number){
    this.meetingService.joinMeeting(meetingId).subscribe({
      next: ()=>{
        this.refreshMeetings()
      },
      error: (err) => console.error('Could not join meeting:', err)
    })
  }


  private refreshMeetings(): void{
    this.filtersSubject.next(this.filtersSubject.getValue())
  }
}
