import { Component } from '@angular/core';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
@Component({
  selector: 'user-table',
  imports: [MatProgressSpinnerModule],
  templateUrl: './user-table.html'
})
export class UserTable {}
