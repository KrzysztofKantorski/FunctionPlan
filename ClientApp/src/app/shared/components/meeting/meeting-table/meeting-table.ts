import { Component, inject, Input } from '@angular/core';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { PastMeetings } from '../../../../core/models/meeting-history';
import { Observable } from 'rxjs';



@Component({
  selector: 'meeting-table',
  imports: [MatTableModule, MatIconModule, DatePipe],
  templateUrl: './meeting-table.html'
})


export class MeetingTable {
  private router = inject(Router);

  //Datesource
  dataSource = new MatTableDataSource<PastMeetings>([]);

  @Input({ required: true }) 
  set data(value: PastMeetings[] | undefined) 
  {
    this.dataSource.data = value ?? [];
  }
  
  displayedColumns: string[] = ['title', 'scheduledFor', 'organizerName', 'actions'];

}
