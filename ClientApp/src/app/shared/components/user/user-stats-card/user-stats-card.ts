import { Component, Input } from '@angular/core';
import { UserStats } from '../../../../core/models/user/userStats';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatCardModule} from '@angular/material/card';
import {MatChipsModule} from '@angular/material/chips';
import {MatButtonModule} from '@angular/material/button';
import { MatCardHeader } from '@angular/material/card';
@Component({
  selector: 'user-stats-card',
  imports: [MatCardModule, MatProgressBarModule, MatChipsModule, MatButtonModule, MatCardHeader],
  templateUrl: './user-stats-card.html'
})
export class UserStatsCard {
  //Require user stats object to be passed
  @Input({ required: true }) title!: string;
  @Input({ required: true }) value!: number | string;
}
