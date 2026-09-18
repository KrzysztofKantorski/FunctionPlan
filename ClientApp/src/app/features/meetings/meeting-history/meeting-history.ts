import { Component, inject } from '@angular/core';
import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { NavbarSearch } from '../../../shared/components/nav/navbar-search/navbar-search';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { Sidebar } from '../../../shared/components/sidebar/sidebar';
import { MeetingTable } from '../../../shared/components/meeting/meeting-table/meeting-table';
import { MeetingFilters } from '../../../core/models/meeting-filters';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
import { MeetingService } from '../../../core/services/meeting-service';
import { MeetingTableView } from '../../../core/models/meeting-table';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'meeting-history',
  imports: [Navbar, NavbarBtnGroup, NavbarSearch, UserMenu, Sidebar, MeetingTable, AsyncPipe],
  templateUrl: './meeting-history.html'
})


export class MeetingHistory {
  
  private meetingService = inject(MeetingService);

  //Filters state
  private filtersSubject = new BehaviorSubject<MeetingFilters>
  ({
    sortOrder: 'asc'
  });

  meetingsHistory$: Observable<MeetingTableView[]> | undefined;



  ngOnInit(): void
  {

    //Send request when filters change
    this.meetingsHistory$ = this.filtersSubject.pipe(

      //Send filters and call service
      switchMap((filters) => this.meetingService.getPastMeetings(filters))
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
  }}
