import { Component } from '@angular/core';
import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { BackButton } from '../../../shared/components/nav/back-button/back-button';
import { MeetingHeader } from '../../../shared/components/meeting-header/meeting-header';
import {MatButtonModule} from '@angular/material/button';


@Component({
  selector: 'app-meeting-info',
  imports: [Navbar, NavbarBtnGroup, UserMenu, BackButton, MeetingHeader, MatButtonModule],
  templateUrl: './meeting-info.html'
})

export class MeetingInfo {}
