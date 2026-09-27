import { ChangeDetectorRef, Component, inject, Input, OnInit, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { DatePipe } from '@angular/common';
import { forkJoin, take } from 'rxjs';



import { Navbar } from '../../../shared/components/nav/navbar/navbar';
import { NavbarBtnGroup } from '../../../shared/components/nav/navbar-btn-group/navbar-btn-group';
import { UserMenu } from '../../../shared/components/nav/user-menu/user-menu';
import { BackButton } from '../../../shared/components/back-button/back-button';
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
import { CommentSection } from '../comment-section/comment-section';
import { Router } from '@angular/router';
import { MeetingDialog } from '../../../shared/components/meeting/meeting-dialog/meeting-dialog';

@Component({
  selector: 'app-meeting-info',
  imports: 
  [
    Navbar, NavbarBtnGroup, UserMenu, BackButton, MeetingHeader, 
    MatButtonModule, MeetingMap, DatePipe, MeetingSubtitle, MeetingText, 
    MatIconModule, UserAvatar, CommentSection
  ],
  templateUrl: './meeting-info.html'
})

export class MeetingInfo implements OnInit {
  private platformId = inject(PLATFORM_ID);
  private meetingService = inject(MeetingService);
  private cdr = inject(ChangeDetectorRef);
  private dialog = inject(MatDialog);
  private router = inject(Router);

  //Get meeting id from route
  @Input() id!: string;

  meetingDetails: MeetingDetails | undefined;
  meetingParticipants: MeetingParticipant[] | undefined;

  isLoading = true;

  isAcceptedView = false;




  ngOnInit() 
  { 

    //Check if accepted view
    const fromUrl = history.state?.fromUrl as string | undefined;

    if(fromUrl?.includes('meeting-accepted'))
    {
      this.isAcceptedView = true;  
    }

    if(isPlatformBrowser(this.platformId)){
      const meetingId = Number(this.id);

      this.meetingService.getMeetingDetails(meetingId).subscribe({
        next: (details) => {
          this.meetingDetails = details;
          this.isLoading = false;
          this.cdr.markForCheck();
        },
        error: (err) => {
          console.error('Cannot get meeting details:', err);
          this.isLoading = false;
        }
      })
    }
  }





  goToMedia(meetingId: number)
  {
    this.router.navigate([`/meeting-info/${meetingId}/media`])
  }





  openParticipantDetails(participant: MeetingParticipant): void 
  {
    this.dialog.open(UserDialog, 
    {
      data: participant,
      width: '320px'
    });
  }




  cancelAttendance()
  {
    const meetingId = Number(this.id)

    this.meetingService.cancelAttendance(meetingId).pipe(take(1)).subscribe({

      next: ()=>{
        this.openMeetingDialog("Meeting attendance has been cancelled");
        this.router.navigate([`/meeting-accepted`])
      },

      error: (err)=>{
       this.openMeetingDialog(`Meeting attendance cannot be cancelled: ${err.message}`)
      }
    })
    
  }





  openMeetingDialog(message: string){
    this.dialog.open(MeetingDialog, 
    {
      data: message,
      width: '320px'
    });
  }

}
