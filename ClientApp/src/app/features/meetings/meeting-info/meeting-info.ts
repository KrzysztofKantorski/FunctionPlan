import { ChangeDetectorRef, Component, inject, Input, OnInit, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { DatePipe } from '@angular/common';
import { forkJoin } from 'rxjs';



import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { BackButton } from '../../../shared/components/nav/back-button/back-button';
import { MeetingHeader } from '../../../shared/components/meeting/meeting-header/meeting-header';
import {MatButtonModule} from '@angular/material/button';
import { MeetingService } from '../../../core/services/meeting-service';
import { MeetingDetails } from '../../../core/models/meeting/meeting-details';
import { MeetingMap } from '../../../shared/components/meeting/meeting-map/meeting-map';
import { MeetingSubtitle } from '../../../shared/components/meeting/meeting-subtitle/meeting-subtitle';
import { MeetingText } from '../../../shared/components/meeting/meeting-text/meeting-text';
import {MatIconModule} from '@angular/material/icon';
import { MeetingParticipant } from '../../../core/models/meeting/meeting-participant';
import { UserAvatar } from '../../../shared/components/meeting/user-avatar/user-avatar';
import { MatDialog } from '@angular/material/dialog';
import { UserDialog } from '../../../shared/components/meeting/user-dialog/user-dialog';


@Component({
  selector: 'app-meeting-info',
  imports: 
  [
    Navbar, NavbarBtnGroup, UserMenu, BackButton, MeetingHeader, 
    MatButtonModule, MeetingMap, DatePipe, MeetingSubtitle, MeetingText, 
    MatIconModule, UserAvatar
  ],
  templateUrl: './meeting-info.html'
})

export class MeetingInfo implements OnInit {
  private platformId = inject(PLATFORM_ID);
  private meetingService = inject(MeetingService);
  private cdr = inject(ChangeDetectorRef);
  private dialog = inject(MatDialog);


  //Get meeting id from route
  @Input() id!: string;

  meetingDetails: MeetingDetails | undefined;
  meetingParticipants: MeetingParticipant[] | undefined;

  isLoading = true;

  ngOnInit() 
  { 
   
    if(isPlatformBrowser(this.platformId)){
       const meetingId = Number(this.id);

        forkJoin({

          //Excecute two request and wait for both responses
          details: this.meetingService.getMeetingDetails(meetingId),
          participants: this.meetingService.getMeetingParticipants(meetingId)

        })
        .subscribe({
          next: ({details, participants}) =>
          {
            this.meetingParticipants = participants;
            this.meetingDetails = details;
            this.isLoading = false;

            //Force browser reload
            this.cdr.detectChanges();
          },

          error: () => 
          {
            this.isLoading = false;
            this.cdr.detectChanges();
          }
        
      })
    }
   
  }



  openParticipantDetails(participant: MeetingParticipant): void 
  {
    this.dialog.open(UserDialog, 
    {
      data: participant,
      width: '320px'
    });
  }
}
