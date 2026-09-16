import { Component, inject, Input, OnInit } from '@angular/core';
import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { BackButton } from '../../../shared/components/nav/back-button/back-button';
import { MeetingHeader } from '../../../shared/components/meeting-header/meeting-header';
import {MatButtonModule} from '@angular/material/button';
import { MeetingService } from '../../../core/services/meeting-service';
import { MeetingDetails } from '../../../core/models/meeting-details';


@Component({
  selector: 'app-meeting-info',
  imports: [Navbar, NavbarBtnGroup, UserMenu, BackButton, MeetingHeader, MatButtonModule],
  templateUrl: './meeting-info.html'
})

export class MeetingInfo implements OnInit {
  private meetingService = inject(MeetingService);

  //Get meeting id from route
  @Input() id!: string;

  meetingDetails: MeetingDetails | undefined;
  isLoading = true;

  ngOnInit() 
  { 
    const meetingId = Number(this.id);

    this.meetingService.getMeetingDetails(meetingId).subscribe({
      next: (data) =>
      {
        this.meetingDetails = data;
        this.isLoading = false;
      },
      error: () => 
      {
        this.isLoading = false;
      }
      
    })
    
  }
}
