import { Component, inject } from '@angular/core';
import { MeetingService } from '../../../core/services/meeting-service';
import { MeetingTableLayout } from '../meeting-table-layout/meeting-table-layout';


@Component({

  selector: 'meeting-organized',
  imports: [MeetingTableLayout],
  templateUrl: './meeting-organized.html'
})
export class MeetingOrganized {
  private meetingService = inject(MeetingService);

  fetchOrganizedMeetings = (filters: any) => this.meetingService.getOrganizedMeetings(filters);
}
