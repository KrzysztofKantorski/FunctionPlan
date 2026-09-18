import { Component, inject } from '@angular/core';
import { MeetingService } from '../../../core/services/meeting-service';
import { MeetingTableLayout } from '../meeting-table-layout/meeting-table-layout';


@Component({
  selector: 'meeting-history',
  imports: [MeetingTableLayout],
  templateUrl: './meeting-history.html'
})


export class MeetingHistory {

  private meetingService = inject(MeetingService);

  fetchPastMeetings = (filters: any) => this.meetingService.getPastMeetings(filters);
}
