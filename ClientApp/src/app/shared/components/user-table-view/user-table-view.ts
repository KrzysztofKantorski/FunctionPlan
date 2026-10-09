import { DatePipe } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Router } from '@angular/router';
import { UserProfile } from '../../../core/models/user/user-model';
import { UserAvatar } from '../meeting/user-avatar/user-avatar';
@Component({
  selector: 'user-table-view',
  imports: [MatTableModule, MatIconModule, DatePipe, UserAvatar],
  templateUrl: './user-table-view.html'
})
export class UserTableView {

  private router = inject(Router);

  //Datesource
  dataSource = new MatTableDataSource<UserProfile>([]);

  displayedColumns: string[] = ['avatar', 'username', 'actions'];

  @Input({ required: true }) 
  set data(value: UserProfile[] | undefined) 
  {
    this.dataSource.data = value ?? [];
  }


}



