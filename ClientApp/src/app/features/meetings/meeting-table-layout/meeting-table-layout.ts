import { Component, Input } from '@angular/core';
import { MeetingFilters } from '../../../core/models/meeting-filters';
import { MeetingTableView } from '../../../core/models/meeting-table';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { NavbarSearch } from '../../../shared/components/nav/navbar-search/navbar-search';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { Sidebar } from '../../../shared/components/sidebar/sidebar';
import { MeetingTable } from '../../../shared/components/meeting/meeting-table/meeting-table';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'meeting-table-layout',
  imports: [Navbar, NavbarBtnGroup, NavbarSearch, UserMenu, Sidebar, MeetingTable, AsyncPipe],
  templateUrl: './meeting-table-layout.html'
})

export class MeetingTableLayout {

  // Function to be passed from view
  @Input({ required: true }) fetchFn!: (filters: MeetingFilters) => Observable<MeetingTableView[]>;

  @Input() defaultSortOrder: 'asc' | 'desc' = 'asc';

  private filtersSubject!: BehaviorSubject<MeetingFilters>;
  meetings$!: Observable<MeetingTableView[]>;

  ngOnInit(): void {
    this.filtersSubject = new BehaviorSubject<MeetingFilters>({
      sortOrder: this.defaultSortOrder
    });

    this.meetings$ = this.filtersSubject.pipe(
      switchMap(filters => this.fetchFn(filters))
    );
  }

  onFiltersUpdated(sidebarFilters: MeetingFilters): void {
    this.filtersSubject.next({
      ...this.filtersSubject.getValue(),
      ...sidebarFilters
    });
  }

  onSearchChanged(searchTerm: string): void {
    this.filtersSubject.next({
      ...this.filtersSubject.getValue(),
      searchTerm
    });
  }
}
