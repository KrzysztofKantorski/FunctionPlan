import { Component, inject } from '@angular/core';
import { MeetingService } from '../../../core/services/meeting-service';
import { MeetingTableLayout } from '../meeting-table-layout/meeting-table-layout';
import { Router } from '@angular/router';


@Component({

  selector: 'meeting-organized',
  imports: [MeetingTableLayout],
  templateUrl: './meeting-organized.html'
})
export class MeetingOrganized {
  private router = inject(Router);
  private meetingService = inject(MeetingService);

  fetchOrganizedMeetings = (filters: any) => this.meetingService.getOrganizedMeetings(filters);


  handleUpdate(meetingId: number): void {
    console.log('Update meeting with id:', meetingId);
  }

  handleDelete(meetingId: number): void {
    console.log('Delete meeting with id:', meetingId);
  }
}
