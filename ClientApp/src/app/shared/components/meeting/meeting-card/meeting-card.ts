import { Component, inject, Input } from '@angular/core';
import { Meeting } from '../../../../core/models/meeting';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatCardModule} from '@angular/material/card';
import {MatChipsModule} from '@angular/material/chips';

import {MatButtonModule} from '@angular/material/button';
import { Router } from '@angular/router';
@Component({
  selector: 'meeting-card',
  imports: [MatCardModule, MatChipsModule, MatProgressBarModule, MatButtonModule],
  templateUrl: './meeting-card.html'
})
export class MeetingCard {
  private router = inject(Router);

  //Reuire meeting object to be passed
  @Input({required: true}) meeting!: Meeting;

  //Navigate to meeting details
  goToDetails(meetingId: number)
  {
    this.router.navigate(['/meeting-info', meetingId])
  }
}
