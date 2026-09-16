import { Component } from '@angular/core';
import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';

@Component({
  selector: 'app-meeting-info',
  imports: [Navbar, NavbarBtnGroup, UserMenu],
  templateUrl: './meeting-info.html'
})

export class MeetingInfo {}
