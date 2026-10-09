import { Component } from '@angular/core';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { UserTableView } from '../../../shared/components/user-table-view/user-table-view';
import { UserProfile } from '../../../core/models/user/user-model';
@Component({
  selector: 'user-table',
  imports: [MatProgressSpinnerModule, UserTableView],
  templateUrl: './user-table.html'
})
export class UserTable {
  mockUsers: UserProfile[] = [
    {
      id: 1,
      username: 'JanKowalski',
      email: 'jan@test.pl',
      profilePictureUrl: null
    },
    {
      id: 2,
      username: 'AnnaNowak',
      email: 'anna@test.pl',
      profilePictureUrl: null
    }
  ];
}
