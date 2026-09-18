import { Component } from '@angular/core';
import { Sidebar } from '../../../shared/components/sidebar/sidebar';
import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { NavbarSearch } from '../../../shared/components/nav/navbar-search/navbar-search';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';


@Component({

  selector: 'meeting-organized',
  imports: [Sidebar, Navbar, NavbarBtnGroup, NavbarSearch, UserMenu],
  templateUrl: './meeting-organized.html'
})
export class MeetingOrganized {}
