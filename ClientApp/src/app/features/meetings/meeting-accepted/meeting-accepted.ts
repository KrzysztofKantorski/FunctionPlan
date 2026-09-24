import { Component, inject, Input } from '@angular/core';
import { MeetingService } from '../../../core/services/meeting-service';
import { MeetingFilters } from '../../../core/models/meeting/meeting-filters';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
import { Meeting } from '../../../core/models/meeting/meeting';
import { MeetingCard } from '../../../shared/components/meeting/meeting-card/meeting-card';
import { AsyncPipe, DatePipe } from '@angular/common';
import { MainHeader } from '../../../shared/components/main-header/main-header';
import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { Sidebar } from '../../../shared/components/sidebar/sidebar';
import { MatButtonModule } from '@angular/material/button';
import { NavbarSearch } from '../../../shared/components/nav/navbar-search/navbar-search';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { MeetingInfoDialog } from '../../../shared/components/meeting/meeting-info-dialog/meeting-info-dialog';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-meeting-accepted',
  imports: [MainHeader, AsyncPipe, Navbar, NavbarBtnGroup, 
    Sidebar, MatButtonModule, NavbarSearch,
    UserMenu, DatePipe, MeetingCard],
  templateUrl: './meeting-accepted.html'
})


export class MeetingAccepted {


  private meetingService = inject(MeetingService);
  private dialog = inject(MatDialog)


  //Filters state
  private filtersSubject = new BehaviorSubject<MeetingFilters>
  ({
    sortOrder: 'asc'
  });
  
  meetings$: Observable <Meeting[]> | undefined;
  
  
  
  ngOnInit(): void
  {
    //Send request when filters change
    this.meetings$ = this.filtersSubject.pipe(
      //Send filters and call service
      switchMap((filters) => this.meetingService.getAcceptedMeetings(filters))
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

  onCancelAttendance(meetingId: number){
    this.meetingService.cancelAttendance(meetingId).subscribe({
      next: ()=>{
        this.openDialog("Meeting attendance has been cancelled")
        this.refreshMeetings();
        console.log("siup super")
      },
      error: (err)=>{
        this.openDialog("Cannot canell meeting attendance")
        console.log("Could not cancel", err)
      }
    })
  }



  private refreshMeetings(): void{
    this.filtersSubject.next(this.filtersSubject.getValue());
  }



  private openDialog(messageToDisplay: string){
    this.dialog.open(MeetingInfoDialog, {
      data: {
        message: messageToDisplay
      },
      width: '380px'
    });
  }
}
