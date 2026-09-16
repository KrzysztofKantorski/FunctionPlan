import { ChangeDetectorRef, Component, inject, Input, OnInit, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { DatePipe } from '@angular/common';



import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { BackButton } from '../../../shared/components/nav/back-button/back-button';
import { MeetingHeader } from '../../../shared/components/meeting-header/meeting-header';
import {MatButtonModule} from '@angular/material/button';
import { MeetingService } from '../../../core/services/meeting-service';
import { MeetingDetails } from '../../../core/models/meeting-details';
import { MeetingMap } from '../../../shared/components/meeting-map/meeting-map';
import { MeetingSubtitle } from '../../../shared/components/meeting-subtitle/meeting-subtitle';
import { MeetingText } from '../../../shared/components/meeting-text/meeting-text';

@Component({
  selector: 'app-meeting-info',
  imports: [Navbar, NavbarBtnGroup, UserMenu, BackButton, MeetingHeader, MatButtonModule, MeetingMap, DatePipe, MeetingSubtitle, MeetingText],
  templateUrl: './meeting-info.html'
})

export class MeetingInfo implements OnInit {
  private platformId = inject(PLATFORM_ID);
  private meetingService = inject(MeetingService);
  private cdr = inject(ChangeDetectorRef);

  //Get meeting id from route
  @Input() id!: string;

  meetingDetails: MeetingDetails | undefined;
  isLoading = true;

  ngOnInit() 
  { 
   
    if(isPlatformBrowser(this.platformId)){
       const meetingId = Number(this.id);
        this.meetingService.getMeetingDetails(meetingId).subscribe({
        next: (data) =>
        {
          this.meetingDetails = data;
          this.isLoading = false;

          //Force browser reload
          this.cdr.detectChanges();
          console.log(this.meetingDetails);
        },

        error: () => 
        {
          this.isLoading = false;
          this.cdr.detectChanges();
        }
        
      })
    }
   
  }
}
