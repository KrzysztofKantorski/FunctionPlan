import { Component } from '@angular/core';
import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { NavbarSearch } from '../../../shared/components/nav/navbar-search/navbar-search';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { Sidebar } from '../../../shared/components/sidebar/sidebar';

@Component({
  selector: 'meeting-history',
  imports: [Navbar, NavbarBtnGroup, NavbarSearch, UserMenu, Sidebar],
  templateUrl: './meeting-history.html'
})
export class MeetingHistory {}
